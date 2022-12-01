using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TravelAgency.Models;

namespace TravelAgency.Dal
{
    public class passenger1Dal : DbContext
    {
        //function that response of from where to take information (model passenger ) to table
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<passenger1>().ToTable("tblpassenger1");
        }
        public DbSet<passenger1> passengerDB { get; set; }


    }
}