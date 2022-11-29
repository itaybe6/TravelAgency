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
        public int row { get; set; }

        [Required]
        public int col { get; set; }

        [Required]
        public Boolean available { get; set; }



    }
}