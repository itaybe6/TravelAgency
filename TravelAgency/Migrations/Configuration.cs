namespace TravelAgency.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TravelAgency.Dal.passenger1Dal>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "TravelAgency.Dal.passenger1Dal";
        }

        protected override void Seed(TravelAgency.Dal.passenger1Dal context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
