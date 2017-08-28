using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anuitex.WebLibrary.Data;
using Anuitex.WebLibrary.Models;

namespace Anuitex.WebLibrary.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View( new BaseModel() {CurrentEntityType = typeof(Book)});
        }
        
    }
}