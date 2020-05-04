namespace EventApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manualUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "EventSelected_EventID", "dbo.Events");
            DropIndex("dbo.Carts", new[] { "EventSelected_EventID" });
            DropColumn("dbo.Carts", "EventID");
            RenameColumn(table: "dbo.Carts", name: "EventSelected_EventID", newName: "EventID");
            AlterColumn("dbo.Carts", "EventID", c => c.Long(nullable: false));
            AlterColumn("dbo.Carts", "EventID", c => c.Long(nullable: false));
            CreateIndex("dbo.Carts", "EventID");
            AddForeignKey("dbo.Carts", "EventID", "dbo.Events", "EventID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "EventID", "dbo.Events");
            DropIndex("dbo.Carts", new[] { "EventID" });
            AlterColumn("dbo.Carts", "EventID", c => c.Long());
            AlterColumn("dbo.Carts", "EventID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Carts", name: "EventID", newName: "EventSelected_EventID");
            AddColumn("dbo.Carts", "EventID", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "EventSelected_EventID");
            AddForeignKey("dbo.Carts", "EventSelected_EventID", "dbo.Events", "EventID");
        }
    }
}
