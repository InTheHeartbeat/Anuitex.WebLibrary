using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anuitex.WebLibrary.Data;
using Anuitex.WebLibrary.Models;
using Anuitex.WebLibrary.ViewHelpers;

namespace Anuitex.WebLibrary.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View(new BaseModel()
            {                
                BreadcrumbModel = new BreadcrumbModel(Url.Action("Index", "Home", null, Request.Url.Scheme))  ,
                CurrentNavSection              = NavSection.Books
            });
        }
        
    }
}