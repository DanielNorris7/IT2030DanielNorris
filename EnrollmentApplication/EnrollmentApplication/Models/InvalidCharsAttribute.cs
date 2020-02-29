using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models
{
    public class InvalidCharsAttribute : ValidationAttribute
    {
        private readonly string _invalidChar;
        public InvalidCharsAttribute(string invalidChar)
            : base("{0} contains unacceptable characters!")
        {
            _invalidChar = invalidChar;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (value.ToString().Contains(_invalidChar))
                {
                    string errorMessage = FormatErrorMessage(validationContext.DisplayName);

                    return new ValidationResult(errorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}