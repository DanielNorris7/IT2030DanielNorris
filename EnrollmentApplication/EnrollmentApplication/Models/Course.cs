using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models
{
    public class Course
    {
        [Display(Name = "Course ID")]
        public virtual long CourseID { get; set; }
        [Required]
        [Display(Name = "Course Title")]
        [MaxLength(length: 150)]
        public virtual string CourseTitle { get; set; }
        [Required]
        [Display(Name = "Description")]
        public virtual string CourseDescription { get; set; }
        [Required]
        [Display(Name = "Number of Credits")]
        [RegularExpression(pattern: "[1234]", ErrorMessage = "Only 1-4 can be entered.")]
        public virtual long CourseCredits { get; set; }
    }
}