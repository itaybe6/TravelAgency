namespace TravelAgency.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class passenger1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblpassenger1",
                c => new
                    {
                        passport = c.Int(nullable: false, identity: true),
                        firstName = c.String(nullable: false, maxLength: 50),
                        lastName = c.String(nullable: false, maxLength: 50),
                        age = c.Int(nullable: false),
                        student = c.String(),
                        userName = c.String(),
                        password1 = c.String(),
                    })
                .PrimaryKey(t => t.passport);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblpassenger1");
        }
    }
}
