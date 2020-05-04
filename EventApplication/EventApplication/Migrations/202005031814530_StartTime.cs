namespace EventApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "StartTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "StartTime");
        }
    }
}
