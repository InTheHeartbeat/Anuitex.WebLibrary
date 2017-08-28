using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Anuitex.WebLibrary.Data;
using Anuitex.WebLibrary.Models;

namespace Anuitex.WebLibrary.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult SignIn()
        {
            return PartialView("SignIn", new SignInModel());
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return PartialView("SignUp", new SignUpModel());
        }

        [HttpPost]
        public ActionResult ComputeSignIn(SignInModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (DataContext.Context.LibraryDataContext.Connection.State != ConnectionState.Open || DataContext.Context.LibraryDataContext.Connection.State != ConnectionState.Connecting)
                    { DataContext.Context.LibraryDataContext.Connection.Open();}
                    Account acc = DataContext.Context.LibraryDataContext.Accounts.FirstOrDefault(
                        ac => ac.Login == model.Login && ac.Hash ==
                              Encoding.UTF8.GetString(MD5.Create()
                                  .ComputeHash(Encoding.UTF8.GetBytes(model.Password))));

                    if (acc == null)
                    {                        
                        ModelState.AddModelError("", "Incorrect login or password");
                        return PartialView("SignIn", model);
                    }

                    DataContext.Context.CurrentUser = acc;
                }
                return PartialView("SignIn", model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                DataContext.Context.LibraryDataContext.Connection.Close();
            }            
        }

        [HttpPost]
        public ActionResult ComputeSignUp(SignUpModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if(DataContext.Context.LibraryDataContext.Connection.State != ConnectionState.Open || DataContext.Context.LibraryDataContext.Connection.State != ConnectionState.Connecting)
                    { DataContext.Context.LibraryDataContext.Connection.Open();}

                    if (DataContext.Context.LibraryDataContext.Accounts.Any(ac => ac.Login == model.Login))
                    {
                        ModelState.AddModelError("Login","Login already exist");
                        return PartialView("SignUp", model);
                    }

                    DataContext.Context.LibraryDataContext.Accounts.InsertOnSubmit(new Account()
                    {
                        Login = model.Login,
                        Hash = Encoding.UTF8.GetString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(model.Password)))
                    });

                    DataContext.Context.LibraryDataContext.SubmitChanges();

                    DataContext.Context.CurrentUser =
                        DataContext.Context.LibraryDataContext.Accounts.FirstOrDefault(ac => ac.Login == model.Login);

                    DataContext.Context.LibraryDataContext.Connection.Close();
                }
                return PartialView("SignUp", model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                DataContext.Context.LibraryDataContext.Connection.Close();
            }
        }

        [AllowAnonymous] 
        [HttpGet]       
        public JsonResult CheckLoginAvailable(string Login)
        {
            try
            {
                if (DataContext.Context.LibraryDataContext.Connection.State != ConnectionState.Open || DataContext.Context.LibraryDataContext.Connection.State != ConnectionState.Connecting)
                { DataContext.Context.LibraryDataContext.Connection.Open();}
                return Json(DataContext.Context.LibraryDataContext.Accounts.FirstOrDefault(u => u.Login == Login) != null,
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                DataContext.Context.LibraryDataContext.Connection.Close();
            }       
        }
    }
}