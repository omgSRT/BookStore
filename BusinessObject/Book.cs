using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public partial class Book
    {
        public Book()
        {
            BookInStores = new HashSet<BookInStore>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Isbn { get; set; }
        public string? Author { get; set; }
        public string? ReleaseYear { get; set; }
        public int? Version { get; set; }
        public bool? IsActive { get; set; }
        public int? Amount { get; set; }
        public int? CategoryId { get; set; }
        public int? PublisherId { get; set; }
        public string? Avatar { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Publisher? Publisher { get; set; }
        public virtual ICollection<BookInStore> BookInStores { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
