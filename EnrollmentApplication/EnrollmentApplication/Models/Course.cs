namespace EnrollmentApplication.Models
{
    public class Course
    {
        public virtual long CourseID { get; set; }
        public virtual string CourseTitle { get; set; }
        public virtual string CourseDescription { get; set; }
        public virtual long CourseCredits { get; set; }
    }
}