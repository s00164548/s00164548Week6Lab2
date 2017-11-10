namespace s00164548Week6Lab2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clubsUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SignupDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.AspNetUsers", "SingupDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "SingupDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.AspNetUsers", "SignupDate");
        }
    }
}
