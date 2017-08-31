using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anuitex.WebLibrary.Data;
using Anuitex.WebLibrary.ViewHelpers;

namespace Anuitex.WebLibrary.Models
{
    public class BaseModel
    {
        public Account CurrentUser => DataContext.Context.CurrentUser;        
        public bool IsCurrentUserAdmin => DataContext.Context.CurrentUser?.Role == 0;        
        public BreadcrumbModel BreadcrumbModel { get; set; }
        public NavSection CurrentNavSection { get; set; }
    }
}