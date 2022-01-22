using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ITIMVCProjectV1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // root for admin  
            routes.MapRoute(
               name: "Admin",
               url: "Dashbord",
               defaults: new { controller = "Admin", action = "Login" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "HomePage", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
