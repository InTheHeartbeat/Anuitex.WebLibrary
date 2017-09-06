using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Anuitex.WebLibrary.Data;
using Anuitex.WebLibrary.Extensions;
using Anuitex.WebLibrary.Models;
using Anuitex.WebLibrary.ViewHelpers;   

namespace Anuitex.WebLibrary.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToActionPermanent("Index", "Home");
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            if (CurrentUser != null) {return RedirectToAction("Index", "Home");}

            return View("SignIn", new SignInModel()
            {
                BreadcrumbModel = new BreadcrumbModel(Url.Action("SignIn", "Account", null, Request.Url.Scheme)),
                CurrentNavSection = NavSection.SignIn,
                CurrentUser = CurrentUser
            });
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            if (CurrentUser != null) { return RedirectToAction("Index", "Home"); }

            return View("SignUp", new SignUpModel()
            {
                BreadcrumbModel = new BreadcrumbModel(Url.Action("SignUp", "Account", null, Request.Url.Scheme)),
                CurrentNavSection = NavSection.SignOut,
                CurrentUser = CurrentUser
            });
        }

        [HttpPost]
        public ActionResult ComputeSignIn(SignInModel model)
        {
            if (CurrentUser != null) { return RedirectToAction("Index", "Home"); }
            if (!ModelState.IsValid) { return View("SignIn", model); }

            Account account = DataContext.Accounts.FirstOrDefault(
                ac => ac.Login == model.Login && ac.Hash == model.Password.MD5());

            if (account == null)
            {
                ModelState.AddModelError("", "Invalid login or password");
                if (model.BreadcrumbModel == null)
                {
                    model.BreadcrumbModel =
                        new BreadcrumbModel(Url.Action("SignIn", "Account", null, Request.Url.Scheme));
                }
                return View("SignIn", model);
            }

            DoLogin(account);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ComputeSignUp(SignUpModel model)
        {
            if (!ModelState.IsValid) { return View("SignUp", model);}

            if (DataContext.Accounts.Any(ac => ac.Login == model.Login))
            {
                ModelState.AddModelError("Login", "Login already exist");
                if (model.BreadcrumbModel == null)
                {
                    model.BreadcrumbModel =
                        new BreadcrumbModel(Url.Action("SignUp", "Account", null, Request.Url.Scheme));
                }
                return View("SignUp", model);
            }

            Account newAccount = new Account()
            {
                Login = model.Login,
                Hash = model.Password.MD5()
            };

            DataContext.Accounts.InsertOnSubmit(newAccount);
            DataContext.SubmitChanges();
            
            ViewBag.SuccessSignUp = true;

            DoLogin(newAccount);

            return RedirectToActionPermanent("Index", "Home");
        }
        

        public ActionResult SignOut()
        {
            AccountAccessRecord previousRecord = CurrentUser?.AccountAccessRecords.FirstOrDefault(r => r.Source == Request.UserHostAddress);
            if (previousRecord != null)
            {
                DataContext.AccountAccessRecords.DeleteOnSubmit(previousRecord);
                DataContext.SubmitChanges();

                HttpCookie at = new HttpCookie("AToken", "");
                at.Expires = DateTime.Now.AddDays(-1d);
                Response.SetCookie(at);
            }
            return RedirectToAction("Index", "Home");
        }

        private void DoLogin(Account account)
        {
            Guid token = Guid.NewGuid();

            AccountAccessRecord previousRecord = account.AccountAccessRecords.FirstOrDefault(r => r.Source == Request.UserHostAddress);
            if (previousRecord != null)
            {
                DataContext.AccountAccessRecords.DeleteOnSubmit(previousRecord);
                DataContext.SubmitChanges();
            }

            AccountAccessRecord record = new AccountAccessRecord() { ActiveDate = DateTime.Now, Account = account, Source = Request.UserHostAddress, Token = token };
            DataContext.AccountAccessRecords.InsertOnSubmit(record);
            DataContext.SubmitChanges();

            HttpCookie at = new HttpCookie("AToken", token.ToString());
            at.Expires = DateTime.Now.AddHours(12);
            Response.SetCookie(at);
        }
    }
}