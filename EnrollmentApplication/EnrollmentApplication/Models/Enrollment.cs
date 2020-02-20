using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models
{
    public class Enrollment
    {
        public virtual long EnrollmentID { get; set; }
        public virtual long StudentID { get; set; }
        public virtual long CourseID { get; set; }
        [Required]
        [RegularExpression(pattern: "[ABCDF]", ErrorMessage = "Only A,B,C,D,F can be entered.")]
        public virtual string Grade { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
        public virtual bool IsActive { get; set; }
        [Required]
        [Display(Name = "Assigned Campus")]
        public virtual string AssignedCampus { get; set; }
        [Required]
        [Display(Name = "Enrolled in Semester")]
        public virtual string EnrollmentSemester { get; set; }
        [Required]
        [Range(minimum: 2018, maximum: 9999)]
        public virtual int EnrollmentYear { get; set; }
    }
}