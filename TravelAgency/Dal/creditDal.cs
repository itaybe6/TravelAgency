using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TravelAgency.Models;

namespace TravelAgency.Dal
{
    public class creditDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<creditCards>().ToTable("tblcreditCards");
        }

        public DbSet<creditCards> creditDB { get; set; }

    }
}



