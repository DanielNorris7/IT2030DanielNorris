namespace EnrollmentApplication.Models
{
    public class Enrollment
    {
        public virtual long EnrollmentID { get; set; }
        public virtual long StudentID { get; set; }
        public virtual long CourseID { get; set; }
        public virtual string Grade { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string AssignedCampus { get; set; }
        public virtual string EnrollmentSemester { get; set; }
        public virtual int EnrollmentYear { get; set; }
    }
}