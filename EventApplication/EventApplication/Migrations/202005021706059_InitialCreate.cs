namespace EventApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventID = c.Long(nullable: false, identity: true),
                        EventTitle = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 150),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Location = c.String(nullable: false),
                        EventTypeID = c.Long(nullable: false),
                        OrganizerName = c.String(nullable: false),
                        OrganizerContactInfo = c.String(),
                        MaxTickets = c.Long(nullable: false),
                        AvailableTickets = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.EventTypes", t => t.EventTypeID, cascadeDelete: true)
                .Index(t => t.EventTypeID);
            
            CreateTable(
                "dbo.EventTypes",
                c => new
                    {
                        EventTypeID = c.Long(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.EventTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "EventTypeID", "dbo.EventTypes");
            DropIndex("dbo.Events", new[] { "EventTypeID" });
            DropTable("dbo.EventTypes");
            DropTable("dbo.Events");
        }
    }
}
