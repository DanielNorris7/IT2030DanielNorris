using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventApplication.Models
{
    public class Event : IValidatableObject
    {
        #region Properties
        public virtual long EventID { get; set; }

        [Required]
        [Display(Name = "Event Title")]
        [MaxLength(length: 50, ErrorMessage = "The Event Title cannot be longer than 50 characters.")]
        public virtual string EventTitle { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(length: 150, ErrorMessage = "The description cannot be longer than 150 characters.")]
        public virtual string Description { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public virtual DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        public virtual DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public virtual DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        public virtual DateTime EndTime { get; set; }

        [Required]
        public virtual string Location { get; set; }

        public virtual EventType EventType { get; set; }

        public virtual long EventTypeID { get; set; }

        [Required]
        [Display(Name = "Organizer Name")]
        public virtual string OrganizerName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Organizer Contact Info")]
        public virtual string OrganizerContactInfo { get; set; }

        [Required]
        [Display(Name = "Max Tickets")]
        public virtual long MaxTickets { get; set; }

        [Required]
        [Display(Name = "Available Tickets")]
        public virtual long AvailableTickets { get; set; }
        #endregion

        #region Methods
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Validate that the Event End Date is not less than the Event Start Date
            if (EndDate.Date < StartDate.Date)
                yield return new ValidationResult("The Events End Date cannot be before the Start Date.");

            // Validate that the Event Start Date is not in the past
            if (StartDate.Date < DateTime.Now.Date)
                yield return new ValidationResult("The Event Start Date cannot be in the past.");

            // Validate that the Event End Date is not in the past
            if (EndDate.Date < DateTime.Now.Date)
                yield return new ValidationResult("The Event End Date cannot be in the past.");

            // Validate that the Max Tickets available is greater than 0
            if (MaxTickets <= 0)
                yield return new ValidationResult("The Max Tickets needs to be greater than 0.");

            // Validate that the Available Tickets is greater than 0
            if (AvailableTickets <= 0)
                yield return new ValidationResult("The Available Tickets needs to be greater than 0.");

            // Validate that the Available Tickets is not more than the Max Tickets
            if (AvailableTickets > MaxTickets)
                yield return new ValidationResult("The Available Tickets cannot be more than the Max Tickets.");
        }
        #endregion
    }
}