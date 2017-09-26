using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Routing;
using Anuitex.WebLibrary.Data;
using Anuitex.WebLibrary.Models;

namespace Anuitex.WebLibrary.Controllers
{
    public class BaseController : Controller
    {
        protected Account CurrentUser { get; private set; }
        protected Visitor CurrentVisitor { get; private set; }

        protected LibraryDataContext DataContext { get; private set; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            DataContext = new LibraryDataContext();

            InitializeCurrentUser(requestContext);
            InitializeCurrentVisitor(requestContext);
        }

        private void InitializeCurrentUser(RequestContext requestContext)
        {
            string at = requestContext.HttpContext.Request.Cookies["AToken"]?.Value;
            string adr = requestContext.HttpContext.Request.UserHostAddress;

            if (!string.IsNullOrWhiteSpace(at) && !string.IsNullOrWhiteSpace(adr))
            {
                Guid token = Guid.Parse(at);
                CurrentUser = DataContext.AccountAccessRecords.FirstOrDefault(t => t.Token == token && t.Source == adr)?.Account;
            }            
        }

        private void InitializeCurrentVisitor(RequestContext requestContext)
        {
            string vt = requestContext.HttpContext.Request.Cookies["VToken"]?.Value;
            if (vt != null)
            {
                Guid guid = Guid.Parse(vt);
                CurrentVisitor = DataContext.Visitors.FirstOrDefault(v => v.Token == guid);
            }

            if (CurrentVisitor == null && CurrentUser == null)
            {
                Guid token = Guid.NewGuid();
                                
                Visitor visitor = new Visitor()
                {
                    Token = token,
                    LastAccess = DateTime.Now,                    
                };

                DataContext.Visitors.InsertOnSubmit(visitor);
                DataContext.SubmitChanges();

                Response.Cookies.Set(new HttpCookie("VToken", token.ToString()));

                CurrentVisitor = visitor;
            }
        }
    }
}