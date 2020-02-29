namespace EnrollmentApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Uodate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "InstuctorName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "InstuctorName");
        }
    }
}
