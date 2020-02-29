namespace EnrollmentApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseID = c.Long(nullable: false, identity: true),
                        CourseTitle = c.String(nullable: false, maxLength: 150),
                        CourseDescription = c.String(nullable: false),
                        CourseCredits = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.CourseID);
            
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        EnrollmentID = c.Long(nullable: false, identity: true),
                        StudentID = c.Long(nullable: false),
                        CourseID = c.Long(nullable: false),
                        Grade = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        AssignedCampus = c.String(nullable: false),
                        EnrollmentSemester = c.String(nullable: false),
                        EnrollmentYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EnrollmentID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentID = c.Long(nullable: false, identity: true),
                        StudentFirstName = c.String(nullable: false, maxLength: 50),
                        StudentLastName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.StudentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollments", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Enrollments", "CourseID", "dbo.Courses");
            DropIndex("dbo.Enrollments", new[] { "CourseID" });
            DropIndex("dbo.Enrollments", new[] { "StudentID" });
            DropTable("dbo.Students");
            DropTable("dbo.Enrollments");
            DropTable("dbo.Courses");
        }
    }
}
