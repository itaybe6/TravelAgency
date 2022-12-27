﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using TravelAgency.Dal;
using TravelAgency.Models;
using TravelAgency.ViewModel;




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

        public ActionResult enterLogin(string user, string password)
        {
            //the admin enetr
            if (user == "12345678" && password == "1")
                return RedirectToAction("adminHome", "admin");

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

        public ActionResult SearchFlight_Roundtrip()
        {
            FlyDal dal = new FlyDal();
            List<Fly> list = dal.FlyDB.ToList<Fly>();
            List<Fly> filterList = new List<Fly>();

            if(Request.Form["dateFrom"] == "")
            {
                //להוסיף תנאי שלא יציג טיסות שכבר היו
                //enter all the flight without date limit
                foreach (Fly fly in list)
                {
                    if (fly.sourceFly == Request.Form["Source"].ToString() && fly.destination == Request.Form["dis"] && fly.aviableSeat > 0 && fly.aviableSeat >= Int32.Parse(Request.Form["numTicket"]))
                        filterList.Add(fly);
                }
            }
            else
            {
                foreach (Fly fly in list)
                {
                    if (fly.sourceFly == Request.Form["Source"].ToString() && Convert.ToDateTime(Request.Form["dateFrom"]) == fly.dateFly && fly.destination == Request.Form["dis"] && fly.aviableSeat > 0 && fly.aviableSeat >= Int32.Parse(Request.Form["numTicket"]))
                        filterList.Add(fly);

                }

            }

            orderDal dalOrder = new orderDal();
            Random rnd = new Random();
            order ord = new order();
            int num = rnd.Next(1, 999999999);

            ord.numberTicket = Int32.Parse(Request.Form["numTicket"]);
            ord.numberOrder = num.ToString();
            ord.checkNum = 1;
            ord.chekin_return = "no";
            ord.endDate = Convert.ToDateTime(Request.Form["dateEnd"]);
            ord.price = 0;
            dalOrder.orderDB.Add(ord);
            dalOrder.SaveChanges();

            TempData["order"] = ord.numberOrder;

            FlyViewModel temp = new FlyViewModel();
            temp.fly = new Fly();
            temp.flyList = filterList;
            temp.increase = filterList.OrderBy(p => p.price).ToList();
            temp.decrease = filterList.OrderByDescending(p => p.price).ToList();


            return View(temp);

        }


        public ActionResult SearchFlight_oneWay()
        {
            FlyDal dal = new FlyDal();
            List<Fly> list = dal.FlyDB.ToList<Fly>();
            List<Fly> filterList = new List<Fly>();


            if (Request.Form["dateFrom"] == "")
            {
                //להוסיף תנאי שלא יציג טיסות שכבר היו
                //enter all the flight without date limit
                foreach (Fly fly in list)
                {
                    if (fly.sourceFly == Request.Form["Source"].ToString() && fly.destination == Request.Form["dis"] && fly.aviableSeat > 0 && fly.aviableSeat >= Int32.Parse(Request.Form["numTicket"]))
                        filterList.Add(fly);
                }
            }
            else{
                foreach (Fly fly in list)
                {
                    if (fly.sourceFly == Request.Form["Source"].ToString() && Convert.ToDateTime(Request.Form["dateFrom"]) == fly.dateFly && fly.destination == Request.Form["dis"] && fly.aviableSeat > 0 && fly.aviableSeat >= Int32.Parse(Request.Form["numTicket"]))
                        filterList.Add(fly);

                }

            }
            
            orderDal dalOrder = new orderDal();
            Random rnd = new Random();
            order ord = new order();
            int num = rnd.Next(1, 999999999);

            ord.numberTicket = Int32.Parse(Request.Form["numTicket"]);
            ord.numberOrder = num.ToString();
            ord.checkNum = 1;
            ord.chekin_return = "yes";
            ord.price = 0;

            dalOrder.orderDB.Add(ord);
            dalOrder.SaveChanges();

            TempData["order"]=ord.numberOrder;
            
            FlyViewModel temp = new FlyViewModel();
            temp.fly = new Fly();
            temp.flyList = filterList;
            temp.increase = filterList.OrderBy(p => p.price).ToList();
            temp.decrease = filterList.OrderByDescending(p => p.price).ToList();

            return View("showFlights_oneWay",temp);

        }

        
        public ActionResult showSeats(string flyNum ,string orderNum)
        {
            seatDal dal = new seatDal();
            List<seat> list = dal.seatDB.ToList<seat>();
            List<seat> filterlist = new List<seat>();

            //for the price of the order
            orderDal dalOrder = new orderDal();
            order order = dalOrder.orderDB.Where(o => o.numberOrder == orderNum).FirstOrDefault();
            FlyDal flydal = new FlyDal();
            Fly f = flydal.FlyDB.Where(o => o.flyNumber== flyNum).FirstOrDefault();
            order.price += f.price;
            order.flyNumber = flyNum;
            dalOrder.SaveChanges();

            TempData["order"] = orderNum;
            TempData["seatNum"] = order.checkNum;

            for ( int i = 1 ; i <= list.Count / 6; i++)
            {
                foreach (seat seat in list)
                {
                    //add all the list with flynum 
                    if (seat.flyNumber == flyNum && seat.rowSeat == i)
                        filterlist.Add(seat);

                }

            }
         
            seatViewModel temp = new seatViewModel();
            temp.seatList = filterlist;

            return View(temp);

        }

        
        public ActionResult personal_details (string flyNum , int row , string col,string orderNum) {

            Ticket ticket= new Ticket();
            ticket.flyNUmber = flyNum;
            ticket.seat = row.ToString() + col;
            ticket.orderNumber = orderNum;

            orderDal orderDal= new orderDal();
            order order = orderDal.orderDB.Where(o => o.numberOrder== orderNum).FirstOrDefault();
            if(order.chekin_return == "yes" && order.checkNum != order.numberTicket)
            {
                seatDal seDal = new seatDal();
                List<seat> list2 = seDal.seatDB.ToList<seat>();
                List<seat> filterlist = new List<seat>();
                FlyDal flyDal = new FlyDal();
                Fly t = flyDal.FlyDB.Where(o => o.flyNumber == ticket.flyNUmber).FirstOrDefault();

                order.checkNum += 1;
                order.price += t.price;
                orderDal.SaveChanges();
                TempData["order"] = ticket.orderNumber;
                TempData["seatNum"] = order.checkNum;

                for (int i = 1; i <= list2.Count / 6; i++)
                {
                    foreach (seat seat in list2)
                    {
                        //add all the list with flynum 
                        if (seat.flyNumber == ticket.flyNUmber && seat.rowSeat == i)
                            filterlist.Add(seat);

                    }

                }
                seatViewModel temp = new seatViewModel();
                temp.seatList = filterlist;
                return View("showSeats", temp);
            }

            else if (order.chekin_return == "yes" && order.checkNum == order.numberTicket)
                return View("payment", order);

            else return View("personal_details", ticket);
        }



        public ActionResult submit_personalDetails(Ticket ticket)
        {
            ticket.passport_flyNumber=ticket.passport + ticket.flyNUmber;
            TicketDal dal = new TicketDal();
            List<Ticket> list = dal.TicketDB.ToList<Ticket>();
            

            //check the object is not exist להוסיף הודעת שגיאה
            foreach (Ticket t in list)
            {
                if (t.passport_flyNumber == ticket.passport_flyNumber)
                {
                    return View("personal_details");
                   
                }

            }

            //save the ticket in db
            dal.TicketDB.Add(ticket);
            dal.SaveChanges();

            int row = Int32.Parse(ticket.seat[0].ToString());
            string col = ticket.seat[1].ToString();

            //change the state of the seat
            seatDal seDal = new seatDal();
            seat seat1 = seDal.seatDB.Where(temp => temp.rowSeat == row && temp.colSeat == col && temp.flyNumber == ticket.flyNUmber).FirstOrDefault();
            seat1.available = "no";
            seDal.SaveChanges();

            
            //move to page according the number of tickets that have to order
            orderDal dalOrder = new orderDal();
            order order = dalOrder.orderDB.Where(o => o.numberOrder == ticket.orderNumber).FirstOrDefault();
            

            if(order.checkNum != order.numberTicket)
            {
                List<seat> list2 = seDal.seatDB.ToList<seat>();
                List<seat> filterlist = new List<seat>();
                FlyDal flyDal = new FlyDal();
                Fly t = flyDal.FlyDB.Where(o => o.flyNumber == ticket.flyNUmber).FirstOrDefault();

                order.checkNum += 1;
                order.price += t.price;
                dalOrder.SaveChanges();
                TempData["order"] = ticket.orderNumber;
                TempData["seatNum"] = order.checkNum;

                for (int i = 1; i <= list2.Count / 6; i++)
                {
                    foreach (seat seat in list2)
                    {
                        //add all the list with flynum 
                        if (seat.flyNumber == ticket.flyNUmber && seat.rowSeat == i)
                            filterlist.Add(seat);

                    }

                }
                seatViewModel temp = new seatViewModel();
                temp.seatList = filterlist;
                return View("showSeats", temp);
            }
            else if(order.checkNum == order.numberTicket && order.chekin_return == "no")
            {
                //now we want to display the return flight
                order.chekin_return= "yes";
                order.checkNum = 1;
                
                TempData["order"] = order.numberOrder;

                //search all the flight that we need
                FlyDal flydal = new FlyDal();
                Fly fly = flydal.FlyDB.Where(t => t.flyNumber == order.flyNumber).FirstOrDefault();
                List<Fly> flyList = flydal.FlyDB.Where(w => w.sourceFly == fly.destination && w.destination == fly.sourceFly && w.dateFly == order.endDate).ToList();


                //send the order list
                FlyViewModel temp = new FlyViewModel();
                temp.fly = new Fly();
                temp.flyList = flyList;
                temp.increase = flyList.OrderBy(p => p.price).ToList();
                temp.decrease = flyList.OrderByDescending(p => p.price).ToList();

                dalOrder.SaveChanges();
                return View("SearchFlight_Roundtrip", temp);

            }
            //end of the order
            else 
            {
                return View("payment",order);

            }
            


        }


        [HttpPost]
        public ActionResult orderFlight(FlyViewModel temp)
        {
            return View("showFlights_oneWay", temp);  
        }



       
    }




}