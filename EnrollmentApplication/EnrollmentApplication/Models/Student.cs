using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models
{
    public class Student
    {
        [Display(Name = "Student ID")]
        public virtual long StudentID { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [MaxLength(length: 50)]
        public virtual string StudentFirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(length: 50)]
        public virtual string StudentLastName { get; set; }
    }
}