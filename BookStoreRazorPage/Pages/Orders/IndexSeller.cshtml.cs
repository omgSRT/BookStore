using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;

namespace BookStoreRazorPage.Pages.Orders
{
    public class IndexSellerModel : PageModel
    {
        private readonly IOrderService _service;

        public IndexSellerModel()
        {
            _service = new OrderService();
        }

        public IList<Order> Order { get;set; } = default!;

        public IActionResult OnGetAsync()
        {
            string role = HttpContext.Session.GetString("account");
            var id = (int)HttpContext.Session.GetInt32("accountId");
            if (role != null )
            {
                if(role.Equals("seller"))
                {
                    Order = _service.GetAllOrdersWithIncludeCustomerAndStaff().OrderByDescending(o => o.CreateDate).ToList();
                    return Page();
                }
            }return RedirectToPage("../Logout");
        }
    }
}
