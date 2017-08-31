using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anuitex.WebLibrary.Data;

namespace Anuitex.WebLibrary.Models
{
    public class BaseModel
    {
        public Account CurrentUser => DataContext.Context.CurrentUser;
        public Type CurrentEntityType { get; set; }
        public bool IsCurrentUserAdmin => DataContext.Context.CurrentUser?.Role == 0;
        public string CurrentEntityName => CurrentEntityType.Name + "s";
        public BreadcrumbModel BreadcrumbModel { get; set; }
    }
}