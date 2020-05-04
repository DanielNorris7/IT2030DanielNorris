using System;
using System.ComponentModel.DataAnnotations;

namespace EventApplication.Models
{
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }

        public string CartId { get; set; }

        public int EventID { get; set; }

        public virtual Event EventSelected { get; set; }

        public int Count { get; set; }

        public DateTime DateCreated { get; set; }
    }
}