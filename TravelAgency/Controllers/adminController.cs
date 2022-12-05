using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelAgency.Dal;
using TravelAgency.Models;

namespace TravelAgency.Controllers
{
    public class adminController : Controller
    {
        // GET: admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult adminHome() {
            return View();
        }

        public ActionResult addFly() {
            return View();

         }

        public ActionResult removeFly() {

            return View();

        }

        public ActionResult editFly()
        {

            return View();

        }


        public ActionResult submit_AddFly(Fly temp)
        {
            //WRONG
           if(temp.sourceFly == temp.destination)
            {
                return null;
            }


            return View("editFly");
        }




    }
}