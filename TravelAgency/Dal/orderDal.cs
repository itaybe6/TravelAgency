using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TravelAgency.Models;


namespace TravelAgency.Dal
{
    public class orderDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<order>().ToTable("tblorder");
        }

        public DbSet<order> orderDB { get; set; }
    }


}




