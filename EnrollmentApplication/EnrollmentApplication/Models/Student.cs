using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models
{
    public class Student : IValidatableObject
    {
        [Display(Name = "Student ID")]
        public virtual long StudentID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MaxLength(length: 50)]
        [InvalidChars(invalidChar: "*")]
        public virtual string StudentFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(length: 50)]
        [InvalidChars(invalidChar: "*", ErrorMessage = "Last Name cannot have any invalid characters.")]
        public virtual string StudentLastName { get; set; }

        [MinimumAge(minimumAge: 20)]
        public virtual int Age { get; set; }

        public virtual string Address1 { get; set; }

        public virtual string Address2 { get; set; }

        public virtual string City { get; set; }

        public virtual string Zipcode { get; set; }

        public virtual string State { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Validation 1: Ensure that Address1 and Address2 are not the same
            if (Equals(Address1, Address2))
                yield return (new ValidationResult("Address2 cannot be the same as Address1"));

            // Validation 2: Ensure that States is two characters long
            if (State.Length > 2)
                yield return (new ValidationResult("Enter a 2 digit State code."));

            // Validation 3: Ensure that the zip code is 5 digits long
            if (Zipcode.Length != 5)
                yield return (new ValidationResult("Enter a 5 digit Zipcode"));
        }
    }
}