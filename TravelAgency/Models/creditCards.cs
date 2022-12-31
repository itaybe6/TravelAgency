using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelAgency.Models
{
    public class creditCards
    {
        [Key]
        [Required]
        [StringLength(16, ErrorMessage = "Must to fill 16 digit")]
        public string number { get; set; }


        [Required]
        public string year { get; set; }


        [Required]
        public string month { get; set; }


        [Required]
        [StringLength(8, ErrorMessage = "Must to fill 8 digit")]
        public string id { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "Must to fill 3 digit")]
        public string cvv { get; set; }



    }
}