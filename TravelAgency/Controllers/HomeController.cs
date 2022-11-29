using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelAgency.Models;

namespace TravelAgency.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult home()
        {
            return View();
        }


        public ActionResult login()
        {
            return View();
        }

        public ActionResult signUp()
        {
            return View();
        }

        public ActionResult MyFlights()
        {
            return View();
        }

        public ActionResult submit_SignUp(passenger temp)
        {

            if (ModelState.IsValid)
            {
                return View("home");
            }

            return View("signUp");
        }

        public ActionResult Enter_Fly_Homepage()
        {
            return View();
        }



    }
}