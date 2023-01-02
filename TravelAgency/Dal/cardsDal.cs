using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TravelAgency.Models;

namespace TravelAgency.Dal
{
    public class cardsDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<cards>().ToTable("tblcreditCard");
        }

        public DbSet<cards> creditDB { get; set; }



    }
}

