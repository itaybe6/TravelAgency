using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelAgency.Models;
using System.ComponentModel.DataAnnotations;


namespace TravelAgency.Models
{
    public class passenger1
    {
        [Key]
        [Required]
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "Passport must to contain 8 numbers!")]
        public int passport { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name between 2-50 latters")]
        public string firstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name between 2-50 latters")]
        public string lastName { get; set; }

        [Required]
        [Range(16, 120, ErrorMessage = "Your age is wrong")]
        public int age { get; set; }

        public string student { get; set; }

        //only for register user
        public string userName { get; set; }
        public string password1 { get; set; }

    }
}