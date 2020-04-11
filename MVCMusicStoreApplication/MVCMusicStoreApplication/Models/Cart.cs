using System;
using System.ComponentModel.DataAnnotations;

namespace MVCMusicStoreApplication.Models
{
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }

        public string CartId { get; set; }

        public int AlbumID { get; set; }

        public virtual Album AlbumSelected { get; set; }

        public int Count { get; set; }

        public DateTime DateCreated { get; set; }
    }
}