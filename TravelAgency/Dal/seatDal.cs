using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TravelAgency.Models;

namespace TravelAgency.Dal
{
    public class seatDal : DbContext
    {
        //function that response of from where to take information (model passenger ) to table
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<seat>().ToTable("tblseat");
        }
        public DbSet<seat> seatDB { get; set; }


    }
}