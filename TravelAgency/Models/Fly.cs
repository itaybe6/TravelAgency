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

        [Key]
        [Required]
        [StringLength(6, ErrorMessage = "Fly number size is 6")]
        public string flyNumber { get; set; }

        public TimeSpan timeFly { get; set; }

        [Required]
        public TimeSpan timeLanding { get; set; }

        [Required]
        public string destination { get; set; }

        [Required]
        public DateTime dateFly { get; set; }

        [Required]
        public DateTime dateLanding { get; set; }

        [Required]
        public string sourceFly { get; set; }

        //seatList size = number of seats
        [Required]
        public int aviableSeat { get; set; }

        [Required]
        public int price { get; set; }

    }
}