using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelAgency.Models
{
    public class order
    {
        [Key]
        [Required]
        public string numberOrder { get; set; }

        public int numberTicket { get; set; }

        public int price { get; set; }

        public string flyNumber { get; set; }

        //number of ticket that have to enter details
        public int checkNum { get; set;}


    }
}