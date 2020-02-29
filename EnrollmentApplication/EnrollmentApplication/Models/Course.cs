using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models
{
    public class Course : IValidatableObject
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
        [Range(minimum: 1, maximum: 4, ErrorMessage = "Only 1-4 can be entered.")]
        public virtual long CourseCredits { get; set; }
        public virtual string InstuctorName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Validation 1: Credits have to be between 1-4
            if (CourseCredits < 1 || CourseCredits > 4)
            {
                yield return (new ValidationResult("Credits must be between 1 and 4."));
            }

            // Validation 2: 
            if (CourseDescription.Split(' ').Length > 100)
            {
                yield return (new ValidationResult("Your description is too verbose."));
            }
        }
    }
}