namespace EventApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShoppingCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        CartId = c.String(),
                        EventID = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        EventSelected_EventID = c.Long(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Events", t => t.EventSelected_EventID)
                .Index(t => t.EventSelected_EventID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "EventSelected_EventID", "dbo.Events");
            DropIndex("dbo.Carts", new[] { "EventSelected_EventID" });
            DropTable("dbo.Carts");
        }
    }
}
