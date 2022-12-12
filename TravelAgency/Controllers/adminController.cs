﻿using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelAgency.Dal;
using TravelAgency.Models;
using TravelAgency.ViewModel;

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

        public ActionResult removeFly( ) {

            return View();

        }

        public ActionResult editFly()
        {
            
            FlyDal dal = new FlyDal();
            List<Fly> list = dal.FlyDB.ToList<Fly>();
            FlyViewModel temp = new FlyViewModel();

            temp.fly = new Fly();
            temp.flyList = list;

            return View(temp);

        }

        
        public ActionResult submit_AddFly(Fly temp)
        {
            //SAME LOCATION TO FLY
           if(temp.sourceFly == temp.destination)
            {
                return null;
            }

            //WRONG DATES
            if (temp.dateLanding < temp.dateFly)
            {
                return null;
            }
           
            if(temp.flyNumber == null)
            {
                return null;

            }
           
            FlyDal dal = new FlyDal();
            dal.FlyDB.Add(temp);
            dal.SaveChanges();

            //enter all the seat to fly
            seatDal dalSeat = new seatDal();     
            for(int i = 1; i <= temp.aviableSeat / 6; i++)
            {
                string id = temp.flyNumber + "_" + i.ToString() + "_" +  "A";
                seat tempSeat = new seat(temp.flyNumber, i, "A", "yes" ,id);
                dalSeat.seatDB.Add(tempSeat);
                dalSeat.SaveChanges();

                id = temp.flyNumber + "_" + i.ToString() + "_"  + "B";
                tempSeat = new seat(temp.flyNumber, i, "B", "yes",id);
                dalSeat.seatDB.Add(tempSeat);
                dalSeat.SaveChanges();

                id = temp.flyNumber + "_" + i.ToString() + "_" + "C";
                tempSeat = new seat(temp.flyNumber, i, "C", "yes", id);
                dalSeat.seatDB.Add(tempSeat);
                dalSeat.SaveChanges();

                id = temp.flyNumber + "_" + i.ToString() + "_" + "D";
                tempSeat = new seat(temp.flyNumber, i, "D", "yes", id);
                dalSeat.seatDB.Add(tempSeat);
                dalSeat.SaveChanges();

                id = temp.flyNumber + "_" + i.ToString() + "_" + "E";
                tempSeat = new seat(temp.flyNumber, i, "E", "yes", id);
                dalSeat.seatDB.Add(tempSeat);
                dalSeat.SaveChanges();

                id = temp.flyNumber + "_" + i.ToString() + "_" + "F";
                tempSeat = new seat(temp.flyNumber, i, "F", "yes", id);
                dalSeat.seatDB.Add(tempSeat);
                dalSeat.SaveChanges();

            }

            return View("addFlySuccess");

            
            }

        public ActionResult addFlySuccess() { return View(); }

         

        public ActionResult deleteFly(string flyNum)
        {
            Fly y = new Fly();
            flyNum = "111111";
            FlyDal dal = new FlyDal();
            List<Fly> list = (from x in dal.FlyDB where x.flyNumber == flyNum select x).ToList();
            foreach (Fly f in list)
            {
                Fly temp = new Fly();
            }
            dal.FlyDB.Remove(y);
            dal.SaveChanges();
           
            return View("addFly");
        }

    }

 }
