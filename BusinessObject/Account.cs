using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public partial class Account
    {
        public Account()
        {
            OrderCustomers = new HashSet<Order>();
            OrderStaffs = new HashSet<Order>();
        }

        public int Id { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Avatar { get; set; }
        [Required]
        [MinLength(8)]
        public string? Phone { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Required]
        public int? RoleId { get; set; }
        public int? StoreId { get; set; }
        public string? Address { get; set; }

        public virtual Role? Role { get; set; }
        public virtual Store? Store { get; set; }
        public virtual ICollection<Order> OrderCustomers { get; set; }
        public virtual ICollection<Order> OrderStaffs { get; set; }
    }
}
