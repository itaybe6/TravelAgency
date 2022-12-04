using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project_TravelAgency
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "signUp",
                url: "passenger1/submit_SignUp",
                defaults: new { controller = "Home", action = "submit_SignUp", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "enterLogin",
               url: "passenger1/enterLogin",
               defaults: new { controller = "Home", action = "enterLogin", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "enterLoginAdmin",
               url: "admin/home",
               defaults: new { controller = "admin", action = "adminHome", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
         



    }
    
}