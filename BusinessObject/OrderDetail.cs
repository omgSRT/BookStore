using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? BookInStoreId { get; set; }
        public int? Quantity { get; set; }
        public int? BookId { get; set; }

        public virtual Book? Book { get; set; }
        public virtual BookInStore? BookInStore { get; set; }
        public virtual Order? Order { get; set; }
    }
}
