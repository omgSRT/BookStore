using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public partial class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public bool? IsActive { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
