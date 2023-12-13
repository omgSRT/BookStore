using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public partial class BookInStore
    {
        public BookInStore()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        [Required]
        public int? BookId { get; set; }
        [Required]
        public int? StoreId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int? Amount { get; set; }

        public virtual Book? Book { get; set; }
        public virtual Store? Store { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
