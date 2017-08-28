using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Anuitex.WebLibrary.Data;
using Anuitex.WebLibrary.Models;

namespace Anuitex.WebLibrary.Controllers
{
    public class BaseController : Controller
    {
        public Account CurrentUser { get; set; }


        
    }
}