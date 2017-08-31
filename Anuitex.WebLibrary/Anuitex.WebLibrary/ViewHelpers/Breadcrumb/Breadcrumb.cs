using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Anuitex.WebLibrary.ViewHelpers
{
    public class Breadcrumb
    {
        public string Text { get; set; }
        public Uri Url { get; set; }
        public string IconCssClass { get; set; }
    }
}