using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelAgency.Models;
using System.ComponentModel.DataAnnotations;


namespace TravelAgency.Models
{
    public class Ticket
    {
        [Key]
        [Required]
        [StringLength(14)]
        public string passport_flyNumber { get; set; }
        
        [Required]
        [StringLength(8, ErrorMessage = "Passport must to be contain 8 digits")]
        public string passport { get; set; }

        [Required]
        [StringLength(6)]
        public string flyNUmber { get; set; }

        [Required]
        [StringLength(3)]
        public string seat { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name between 2-50 latters")]
        public string firstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name between 2-50 latters")]
        public string lastName { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string orderNumber { get; set; }



    }
}