using System.ComponentModel.DataAnnotations;

namespace EventFinder.Models
{
    public class EventType
    {
        #region Properties
        public virtual int EventTypeID { get; set; }

        [Required]
        [MaxLength(length: 50, ErrorMessage = "The Event Type cannot be longer than 50 characters.")]
        public virtual string Type { get; set; }
        #endregion
    }
}