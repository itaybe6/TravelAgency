using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TravelAgency.Models;

namespace TravelAgency.Dal
{
    public class FlyDal : DbContext
    {
        //function that response of from where to take information (model passenger ) to table
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Fly>().ToTable("tblFly");
        }
        public DbSet<Fly> FlyDB { get; set; }


    }
}