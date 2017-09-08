using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Anuitex.WebLibrary
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );

            routes.MapRoute(
                "EditJournal",
                "Journals/EditJournal/{id}",
                new
                {
                    controller = "Journals",
                    action = "EditJournal",
                    id = UrlParameter.Optional
                });
            routes.MapRoute(
                "EditJournalErr",
                "Journals/EditJournal",
                new
                {
                    controller = "Journals",
                    action = "Index",
                });

            routes.MapRoute(
                "EditBook",
                "Books/EditBook/{id}",
                new
                {
                    controller = "Books",
                    action = "EditBook",
                    id = UrlParameter.Optional
                });
            routes.MapRoute(
                "EditBookErr",
                "Books/EditBook",
                new
                {
                    controller = "Books",
                    action = "Index",
                });
        }
    }
}
