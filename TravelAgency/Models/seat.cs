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
        public string colSeat { get; set; }

        [Required]
        public string available { get; set; }

        [Key]
        public string id  { get; set; }    

        public TimeSpan timeForSave { get; set; }
        public seat(string t1, int row, string col, string available,string id)
        {
            this.flyNumber = t1;
            this.rowSeat = row;
            this.colSeat = col;
            this.available = available;
            this.id = id;
            timeForSave = new TimeSpan();
        }

        public seat()
        {

        }


    }
}

   