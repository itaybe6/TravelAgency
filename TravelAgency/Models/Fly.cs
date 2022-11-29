using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelAgency.Models;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models
{
    public class Fly
    {
        // get for the send informaion 
        //set for set informaion
        //[Required] - user must to put

        [Required]
        public DateTime time { get; set; }

        [Required]
        public string destination { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string from { get; set; }



        //CountrysConnection size = Number connections
        [Required]
        public int NumberConnections { get; set; }
        public List<String> CountrysConnection = new List<string>();



        //seatList size = number of seats
        [Required]
        public int numberOfSeets { get; set; }
        [Required]
        List<seat> seatList = new List<seat>();


        [Required]
        public int price { get; set; }


        [Required]
        public string company { get; set; }



    }
}