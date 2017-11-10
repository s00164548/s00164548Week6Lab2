namespace s00164548Week6Lab2.Migrations.ClubModelMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialiseClubs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClubEvent",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        Venue = c.String(),
                        Location = c.String(),
                        ClubId = c.Int(nullable: false),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.Club", t => t.ClubId, cascadeDelete: true)
                .Index(t => t.ClubId);
            
            CreateTable(
                "dbo.Club",
                c => new
                    {
                        ClubId = c.Int(nullable: false, identity: true),
                        ClubName = c.String(),
                        CreationDate = c.DateTime(nullable: false, storeType: "date"),
                        adminID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClubId);
            
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        memberID = c.Guid(nullable: false),
                        StudentID = c.Guid(nullable: false),
                        approved = c.Boolean(nullable: false),
                        Club_ClubId = c.Int(),
                        ClubEvent_EventID = c.Int(),
                    })
                .PrimaryKey(t => new { t.memberID, t.StudentID })
                .ForeignKey("dbo.Club", t => t.Club_ClubId)
                .ForeignKey("dbo.ClubEvent", t => t.ClubEvent_EventID)
                .Index(t => t.Club_ClubId)
                .Index(t => t.ClubEvent_EventID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Member", "ClubEvent_EventID", "dbo.ClubEvent");
            DropForeignKey("dbo.ClubEvent", "ClubId", "dbo.Club");
            DropForeignKey("dbo.Member", "Club_ClubId", "dbo.Club");
            DropIndex("dbo.Member", new[] { "ClubEvent_EventID" });
            DropIndex("dbo.Member", new[] { "Club_ClubId" });
            DropIndex("dbo.ClubEvent", new[] { "ClubId" });
            DropTable("dbo.Member");
            DropTable("dbo.Club");
            DropTable("dbo.ClubEvent");
        }
    }
}
