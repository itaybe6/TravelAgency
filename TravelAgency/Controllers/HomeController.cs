using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelAgency.Dal;
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

        public ActionResult submit_SignUp(passenger1 temp)
        {

            if (ModelState.IsValid)
            {
                passenger1Dal dal = new passenger1Dal();        
                dal.passengerDB.Add(temp);
                dal.SaveChanges();
                return View("successPage",temp);
            }

            else return View("signUp");
        }

        public ActionResult enterLogin(int user, string password)
        {
            //the admin enetr
            if(user == 6 && password == "1")
                return RedirectToAction("home","admin");

            passenger1Dal dal = new passenger1Dal();
            List<passenger1> temp = dal.passengerDB.ToList<passenger1>();
            foreach (passenger1 obj in temp)
            {
                if(obj.passport == user && obj.password1 == password) 
                    return View("home");

            }


            return View("login");
        }


        public ActionResult Enter_Fly_Homepage()
        {
            return View();
        }

    
    }
}