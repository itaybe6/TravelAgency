using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public void check_seatsSave()
        {
            seatDal seatDal= new seatDal();
            TimeSpan tempTime = new TimeSpan(DateTime.Now.Ticks);
            List<seat> seats= seatDal.seatDB.Where(s => s.available == "blk").ToList();

            foreach(seat s in seats)
            {
                
                if(tempTime.TotalMinutes - s.timeForSave.TotalMinutes >= 30)
                {
                    seatDal.seatDB.Where(seat => seat.flyNumber == s.flyNumber && seat.rowSeat == s.rowSeat && seat.colSeat == s.colSeat ).FirstOrDefault().available = "yes";
                    seatDal.seatDB.Where(seat => seat.flyNumber == s.flyNumber && seat.rowSeat == s.rowSeat && seat.colSeat == s.colSeat).FirstOrDefault().timeForSave = default(TimeSpan); 
                    seatDal.SaveChanges();
                }
            }



        }
        public ActionResult Index()
        {
            check_seatsSave();
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
            List<Fly> filterList;
            List<Fly> filterList2;// to return flights
            string source = Request.Form["Source"].ToString();
            string dis = Request.Form["dis"].ToString();
            int numTicket = Int32.Parse(Request.Form["numTicket"]);
            DateTime dateFly = DateTime.Parse(Request.Form["dateFrom"]);
            DateTime dateReturn = DateTime.Parse(Request.Form["dateEnd"]);


            //if no exist any flight
            filterList = dal.FlyDB.Where(f => f.sourceFly == source && f.destination == dis && f.aviableSeat >= numTicket && f.dateFly == dateFly ).ToList();
            if (filterList.Count() == 0)
            {
                return View("noFlights");
            }

            //if no exist any flight in the return 
            filterList2 = dal.FlyDB.Where(f => f.sourceFly == dis && f.destination == source && f.aviableSeat >= numTicket &&  f.dateFly == dateReturn).ToList();
            if (filterList2.Count() == 0)
            {
                return View("noFlightsReturn");
            }



            //to rate the flights
            int rated;
            foreach (Fly fly in filterList)
            {
                rated = ((fly.flightSeat - fly.aviableSeat) * 1000) / fly.flightSeat;
                dal.FlyDB.Where(f => f.flyNumber == fly.flyNumber).FirstOrDefault().rated = rated;

            }
            dal.SaveChanges();

            orderDal dalOrder = new orderDal();
            Random rnd = new Random();
            order ord = new order();
            int num = rnd.Next(1, 999999999);

            ord.numberTicket = numTicket;
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
            temp.rated = filterList.OrderByDescending(p => p.rated).ToList();


            return View(temp);

        }


        public ActionResult SearchFlight_oneWay()
        {
            FlyDal dal = new FlyDal();
            List<Fly> filterList;

            string source = Request.Form["Source"].ToString();
            string dis = Request.Form["dis"].ToString();
            int numTicket = Int32.Parse(Request.Form["numTicket"]);
            DateTime dateTime = DateTime.Now;
            DateTime dateFly;


            //if the user dont enter flight date
            if (Request.Form["dateFrom"] == "")
            {
                filterList = dal.FlyDB.Where(f => f.sourceFly == source && f.destination == dis && f.aviableSeat >= numTicket && f.dateFly > dateTime).ToList();
            }
            else
            {
                dateFly= DateTime.Parse(Request.Form["dateFrom"]);
                filterList = dal.FlyDB.Where(f => f.sourceFly == source && f.destination == dis && f.aviableSeat >= numTicket && f.dateFly > dateTime && f.dateFly == dateFly && f.dateFly > dateTime).ToList();
            }

            //if no exist ant flight
            if(filterList.Count() == 0)
            {
                return View("noFlights");
            }

            //rated the flights
            int rated;
            foreach (Fly fly in filterList)
            {
                rated = ((fly.flightSeat - fly.aviableSeat) * 1000) / fly.flightSeat;  
                dal.FlyDB.Where(f => f.flyNumber == fly.flyNumber).FirstOrDefault().rated= rated;

            }
            dal.SaveChanges();
            


            orderDal dalOrder = new orderDal();
            Random rnd = new Random();
            order ord = new order();
            int num = rnd.Next(1, 999999999);

            ord.numberTicket = Int32.Parse(Request.Form["numTicket"]);
            ord.numberOrder = num.ToString();
            ord.checkNum = 1;
            ord.price = 0;
            ord.flyNumber2 = null; //no return flight
            dalOrder.orderDB.Add(ord);
            dalOrder.SaveChanges();

            TempData["order"]=ord.numberOrder;
            
            FlyViewModel temp = new FlyViewModel();
            temp.fly = new Fly();
            temp.flyList = filterList;
            temp.increase = filterList.OrderBy(p => p.price).ToList();
            temp.decrease = filterList.OrderByDescending(p => p.price).ToList();
            temp.rated = filterList.OrderByDescending(p => p.rated).ToList();


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

            if(order.chekin_return != "yes")
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
           

            orderDal orderDal = new orderDal();
            order order = orderDal.orderDB.Where(o => o.numberOrder == orderNum).FirstOrDefault();

            //adding ticket for the reatun way
            if(order.chekin_return == "yes")
            {
                orderDal.orderDB.Where(o => o.numberOrder == orderNum).FirstOrDefault().flyNumber2 = flyNum;
                orderDal.SaveChanges();
                TicketDal dal = new TicketDal();
                Ticket tic = dal.TicketDB.Where(ti => ti.passport_flyNumber_ordernumber.Length == 1).FirstOrDefault();
                Ticket tempT = new Ticket();
                tempT.passport = tic.passport;
                tempT.flyNUmber = flyNum;
                tempT.orderNumber = tic.orderNumber;
                tempT.pay = "no";
                tempT.firstName = tic.firstName;
                tempT.lastName = tic.lastName;
                tempT.passport_flyNumber_ordernumber = tic.passport + "_" + flyNum + "_" + tic.orderNumber;
                tempT.seat = row.ToString() + col;
                dal.TicketDB.Remove(tic);
                dal.TicketDB.Add(tempT);
                dal.SaveChanges();
            }


            FlyDal flyDal = new FlyDal();
            Fly t = flyDal.FlyDB.Where(o => o.flyNumber == ticket.flyNUmber).FirstOrDefault();

            if (order.chekin_return == "yes" && order.checkNum != order.numberTicket)
            {

                seatDal seDal = new seatDal();
                List<seat> list2 = seDal.seatDB.ToList<seat>();
                List<seat> filterlist = new List<seat>();
               
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
            {

                seatDal seDal = new seatDal();
                TempData["flynum2"] = t.flyNumber;
                TempData["date2"] = t.dateFly.ToShortDateString();
                TempData["time2"] = t.timeFly;
                TempData["price2"] = t.price;


                t = flyDal.FlyDB.Where(o => o.flyNumber == order.flyNumber).FirstOrDefault();
                TempData["flynum1"] = t.flyNumber;
                TempData["date1"] = t.dateFly.ToShortDateString();
                TempData["time1"] = t.timeFly;
                TempData["price1"] = t.price;
                TempData["source"] = t.sourceFly;
                TempData["dis"] = t.destination;

                return View("payment", order);
            }
            else return View("personal_details", ticket);
        }



        public ActionResult submit_personalDetails(Ticket ticket)
        {
            ticket.passport_flyNumber_ordernumber=ticket.passport + "_" + ticket.flyNUmber + "_" + ticket.orderNumber;
            ticket.pay = "no";
            TicketDal dal = new TicketDal();
            List<Ticket> list = dal.TicketDB.ToList<Ticket>();
            

            //check the object is not exist להוסיף הודעת שגיאה
            foreach (Ticket t in list)
            {
                if (t.passport == ticket.passport && ticket.pay == "yes")
                {
                    ViewData["haveTicket"] = "yes";
                    return View("personal_details");
                   
                }

            }

            //save the ticket in db
            dal.TicketDB.Add(ticket);
            dal.SaveChanges();




            //change the state of the seat
            seatDal seDal = new seatDal();
            

            //move to page according the number of tickets that have to order
            orderDal dalOrder = new orderDal();
            order order = dalOrder.orderDB.Where(o => o.numberOrder == ticket.orderNumber).FirstOrDefault();
            


            if(order.chekin_return == "no")
            {
                Ticket tempT = new Ticket();
                tempT.passport = ticket.passport;
                tempT.flyNUmber = ticket.flyNUmber;
                tempT.orderNumber = ticket.orderNumber;
                tempT.pay = "no";
                tempT.firstName= ticket.firstName;
                tempT.lastName= ticket.lastName;
                tempT.passport_flyNumber_ordernumber = order.checkNum.ToString();
                tempT.seat = "A1";
                dal.TicketDB.Add(tempT);
                dal.SaveChanges();
            }



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

                //to rate the flights
                int rated;
                foreach (Fly fly1 in flyList)
                {
                    rated = ((fly1.flightSeat - fly1.aviableSeat) * 1000) / fly1.flightSeat;
                    flydal.FlyDB.Where(f => f.flyNumber == fly1.flyNumber).FirstOrDefault().rated = rated;

                }
                dal.SaveChanges();

                //send the order list
                FlyViewModel temp = new FlyViewModel();
                temp.fly = new Fly();
                temp.flyList = flyList;
                temp.increase = flyList.OrderBy(p => p.price).ToList();
                temp.decrease = flyList.OrderByDescending(p => p.price).ToList();
                temp.rated = flyList.OrderByDescending(p => p.rated).ToList();

                dalOrder.SaveChanges();
                return View("SearchFlight_Roundtrip", temp);

            }
            //end of the order
            else 
            {
                FlyDal flydal = new FlyDal();
                Fly fly = flydal.FlyDB.Where(t => t.flyNumber == order.flyNumber).FirstOrDefault();

                TempData["flynum1"] = fly.flyNumber;
                TempData["date1"] = fly.dateFly.ToShortDateString();
                TempData["time1"] = fly.timeFly;
                TempData["price1"] = fly.price;
                TempData["source"] = fly.sourceFly;
                TempData["dis"] = fly.destination;


                return View("payment",order);

            }

        }


        [HttpPost]
        public ActionResult orderFlight(FlyViewModel temp)
        {
            return View("showFlights_oneWay", temp);  
        }


        public ActionResult submitPay()
        {
            //change the seats aviable acording the flight number
            string numberOrder = Request.Form["numberOrder"];
            orderDal orderD = new orderDal();
            order order = orderD.orderDB.Where(o => o.numberOrder== numberOrder).FirstOrDefault();

            FlyDal flyDal = new FlyDal();

           flyDal.FlyDB.Where(f=>f.flyNumber == order.flyNumber).FirstOrDefault().aviableSeat -= order.numberTicket;

            if (order.flyNumber2 != null)
                flyDal.FlyDB.Where(f => f.flyNumber == order.flyNumber2).FirstOrDefault().aviableSeat -= order.numberTicket;

            flyDal.SaveChanges();


            //change the state of all the seat from green to red
            TicketDal ticketDal = new TicketDal();
            List<Ticket> ticketList = ticketDal.TicketDB.Where(tic => tic.orderNumber == numberOrder).ToList();
            seatDal seatD = new seatDal();
            seat tempSeat = new seat();
            int row;
            string col;
            foreach (Ticket ticket in ticketList)
            {
                ticketDal.TicketDB.Where(t => t.passport_flyNumber_ordernumber == ticket.passport_flyNumber_ordernumber).FirstOrDefault().pay = "yes";
                ticketDal.SaveChanges();
                //2C or 14B
                if (ticket.seat.Length == 2)
                {
                    col = ticket.seat[1].ToString();
                    row = Int32.Parse(ticket.seat[0].ToString());
                }
                else
                {
                    row = Int32.Parse(ticket.seat.Substring(0, 2).ToString());
                    col = ticket.seat[2].ToString();

                }
                tempSeat = seatD.seatDB.Where(s => s.flyNumber == ticket.flyNUmber && s.colSeat == col && s.rowSeat == row).FirstOrDefault();
                tempSeat.available = "no";
                seatD.SaveChanges();

            }



            //if the user want to save is details
            if (Request.Form["keep"] == "on")
            {
                cardsDal dal = new cardsDal();
                cards creadit = new cards();
                creadit.number = Request.Form["cardNumber"];
                creadit.month = Request.Form["month"].ToString();
                creadit.year = Request.Form["year"].ToString();
                creadit.id = Request.Form["id"];
                creadit.cvv = Request.Form["cvv"].ToString();
                creadit.firstName = Request.Form["firstName"].ToString();
                creadit.lastName = Request.Form["lastName"].ToString();
                dal.creditDB.Add(creadit);
                dal.SaveChanges();

            }
            return View("paySuccess");
        }


        [HttpPost]
        public ActionResult searchCard(string id) 
        {
            cardsDal dal = new cardsDal();

            foreach(cards card in dal.creditDB.ToList())
            {
                if(card.id == id)
                {
                      return Json(new { status = "true", card = card.number ,card.cvv});
                }

            }


            return Json(new { status = "false" });
        }


        public ActionResult payment()
        {
            return View();
        }

        public ActionResult paySuccess(string numberOrder)
        {
            orderDal orderD = new orderDal();
            order order = orderD.orderDB.Where(o => o.numberOrder == numberOrder).FirstOrDefault();

            FlyDal flyDal = new FlyDal();

            int num = flyDal.FlyDB.Where(f => f.flyNumber == order.flyNumber).FirstOrDefault().aviableSeat;
            num -= order.numberTicket;
            flyDal.FlyDB.Where(f => f.flyNumber == order.flyNumber).FirstOrDefault().aviableSeat = num ;

            //only for round trip flight
            if (order.flyNumber2 != null)
                flyDal.FlyDB.Where(f => f.flyNumber == order.flyNumber2).FirstOrDefault().aviableSeat -= order.numberTicket;

            flyDal.SaveChanges();

            //change the state of all the seat from green to red
            TicketDal ticketDal = new TicketDal();
            List<Ticket> ticketList = ticketDal.TicketDB.Where(tic => tic.orderNumber == numberOrder).ToList();
            seatDal seatD = new seatDal();
            seat tempSeat = new seat();
            int row;
            string col;
            foreach (Ticket ticket in ticketList)
            {
                ticketDal.TicketDB.Where(t => t.passport_flyNumber_ordernumber == ticket.passport_flyNumber_ordernumber).FirstOrDefault().pay = "yes";
                ticketDal.SaveChanges();
                //2C or 14B
                if (ticket.seat.Length == 2)
                {
                    col = ticket.seat[1].ToString();
                    row = Int32.Parse(ticket.seat[0].ToString());
                }
                else
                {
                    row = Int32.Parse(ticket.seat.Substring(0, 2).ToString());
                    col = ticket.seat[2].ToString();

                }
                tempSeat = seatD.seatDB.Where(s => s.flyNumber == ticket.flyNUmber && s.colSeat == col && s.rowSeat == row).FirstOrDefault();
                tempSeat.available = "no";
                seatD.SaveChanges();

            }
            return View();
        }

        public ActionResult ShowTicket()
        {
            string passport = Request.Form["pas"];
            
            TicketDal ticDal = new TicketDal();

            List<Ticket> list = ticDal.TicketDB.Where(t => t.passport == passport && t.pay =="yes").ToList();

            if(list.Count == 0) { 
                ViewData["noTicket"] = "yes";
                return View("MyFlights");
 
            }

            FlyDal flyDal = new FlyDal();
            List<Fly> listFly= new List<Fly>();
            Fly temp = new Fly();
          

            foreach( Ticket ticket in list)
            {
                temp = flyDal.FlyDB.Where(f=>f.flyNumber == ticket.flyNUmber).FirstOrDefault();
                listFly.Add(temp);

            }

            FlyViewModel F = new FlyViewModel();
            F.flyList = listFly;

            return View(F);  
        }


    }




}