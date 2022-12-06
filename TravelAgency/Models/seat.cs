using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelAgency.Models;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models
{
    public class seat
    {

        [Required]
        public string flyNumber { get; set; }

        [Required]
        public int rowSeat { get; set; }

        [Required]
        public int colSeat { get; set; }

        [Required]
        public string available { get; set; }



    }
}