using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Anuitex.WebLibrary.ViewHelpers;
using WebGrease.Css.Extensions;

namespace Anuitex.WebLibrary.Models
{
    public class BreadcrumbModel
    {        
        public List<Breadcrumb> Breadcrumbs { get; set; }
        public BreadcrumbModel(string url)
        {
            Uri uri = new Uri(url);
            Breadcrumbs = new List<Breadcrumb>();
            if (uri.Segments.Length > 1)
            {
                for (int i = 1; i < uri.Segments.Length + 1; i++)
                {
                    string text = "";

                    foreach (char ch in uri.Segments[i - 1].Replace("/", ""))
                    {
                        if (Char.IsUpper(ch))
                        {
                            text += " " + ch;
                        }
                        else
                        {
                            text += ch;
                        }
                    }

                    string breadUri;

                    if (i < uri.Segments.Length)
                    {
                        breadUri = uri.AbsoluteUri.Remove(
                            uri.AbsoluteUri.IndexOf(uri.Segments[i],
                                StringComparison.Ordinal));
                    }
                    else
                    {
                        breadUri = uri.AbsoluteUri;
                    }

                    Breadcrumbs.Add(new Breadcrumb()
                    {
                        Url = new Uri(breadUri),
                        IconCssClass = "ic-" + (uri.Segments[i - 1].Replace("/","").Length == 0 ? "home" : uri.Segments[i-1].ToLower()),
                        Text = uri.Segments[i - 1].Replace("/", "").Length == 0 ? "Home" : text
                    });
                }
            }
            else
            {
                Breadcrumbs.Add(new Breadcrumb()
                {
                    Url = new Uri(uri.AbsoluteUri),
                    IconCssClass = "ic-home",
                    Text = "Home"
                });
            }            
        }
    }
}