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
    public class Index_SellerModel : PageModel
    {
        private readonly IOrderService _service;

        public Index_SellerModel()
        {
            _service = new OrderService();
        }

        public IList<Order> Order { get;set; } = default!;

        public IActionResult OnGetAsync()
        {
            string role = HttpContext.Session.GetString("account");
            int id = (int)HttpContext.Session.GetInt32("accountId");
            if (role != null )
            {
                if(role.Equals("seller"))
                {
                    Order = _service.GetAllOrdersWithIncludeCustomerAndStaff();
                    return Page();
                }
            }return RedirectToPage("../Login");
        }
    }
}
