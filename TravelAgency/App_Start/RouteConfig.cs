using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

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
               name: "deleteFly",
               url: "Views/admin/delete/{flyNum}",
               defaults: new { controller = "admin", action = "submitRemoveFly", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "editFly",
              url: "Views/admin/addEditFly/{flyNum}",
              defaults: new { controller = "admin", action = "addEditFly", id = UrlParameter.Optional }
          );

            routes.MapRoute(
             name: "submitEditFly",
             url: "Views/admin/submit_EditFly/{temp}",
             defaults: new { controller = "admin", action = "submit_EditFly", id = UrlParameter.Optional }
         );

            routes.MapRoute(
            name: "showSeats",
            url: "Views/Home/ShowSeats/{flyNum}/{orderNum}",
            defaults: new { controller = "Home", action = "showSeats", id = UrlParameter.Optional }
        );


            routes.MapRoute(
            name: "personalDtails",
            url: "Views/Home/personal_details/{flyNum}/{row}/{col}/{orderNum}",
            defaults: new { controller = "Home", action = "personal_details", id = UrlParameter.Optional }
        );

            routes.MapRoute(
            name: "submit_personalDetails",
            url: "Ticket/submit_personalDetails",
            defaults: new { controller = "Home", action = "submit_personalDetails", id = UrlParameter.Optional }
        );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


            


        }



         



    }
    
}