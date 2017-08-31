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
using Anuitex.WebLibrary.ViewHelpers;

namespace Anuitex.WebLibrary.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToActionPermanent("Index", "Home");
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View("SignIn", new SignInModel()
            {
                BreadcrumbModel = new BreadcrumbModel(Url.Action("SignIn", "Account", null, Request.Url.Scheme))                
            });
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View("SignUp", new SignUpModel()
            {
                BreadcrumbModel = new BreadcrumbModel(Url.Action("SignUp", "Account", null, Request.Url.Scheme))       
            });
        }

        [HttpPost]
        public ActionResult ComputeSignIn(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                Account account = DataContext.Context.LibraryDataContext.Accounts.FirstOrDefault(
                    ac => ac.Login == model.Login && ac.Hash ==
                          Encoding.UTF8.GetString(MD5.Create()
                              .ComputeHash(Encoding.UTF8.GetBytes(model.Password))));

                if (account == null)
                {
                    ModelState.AddModelError("", "Invalid login or password");
                    return View("SignIn",model);
                }

                DataContext.Context.CurrentUser = account;
                ViewBag.SuccessSignIn = true;
            }
            return RedirectToActionPermanent("Index", "Home");
        }

        [HttpPost]
        public ActionResult ComputeSignUp(SignUpModel model)
        {
            if (!ModelState.IsValid) return View("SignUp", model);

            if (DataContext.Context.LibraryDataContext.Accounts.Any(ac => ac.Login == model.Login))
            {
                ModelState.AddModelError("Login", "Login already exist");
                return View("SignUp", model);
            }

            DataContext.Context.LibraryDataContext.Accounts.InsertOnSubmit(new Account()
            {
                Login = model.Login,
                Hash = Encoding.UTF8.GetString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(model.Password)))
            });

            DataContext.Context.LibraryDataContext.SubmitChanges();

            DataContext.Context.CurrentUser =
                DataContext.Context.LibraryDataContext.Accounts.FirstOrDefault(ac => ac.Login == model.Login);
            ViewBag.SuccessSignUp = true;

            return RedirectToActionPermanent("Index", "Home");
        }

        [AllowAnonymous] 
        [HttpGet]       
        public JsonResult CheckLoginAvailable(string Login)
        {
            return Json(DataContext.Context.LibraryDataContext.Accounts.FirstOrDefault(u => u.Login == Login) != null,
                    JsonRequestBehavior.AllowGet);            
        }

        public ActionResult SignOut()
        {
            DataContext.Context.CurrentUser = null;
            return RedirectToAction("Index", "Home");
        }
    }
}