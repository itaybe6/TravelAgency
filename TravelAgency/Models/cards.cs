using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TravelAgency.Dal;


namespace TravelAgency.Models
{
    public class cards
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


        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name between 2-50 latters")]
        public string firstName { get; set; }


        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name between 2-50 latters")]
        public string lastName { get; set; }



    }
}

