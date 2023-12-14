﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;

namespace BookStoreRazorPage.Pages.OrderDetails
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _service;

        public IndexModel()
        {
            _service = new OrderService();
        }

        public IList<OrderDetail> OrderDetail { get; set; } = default!;

        public IActionResult OnGetAsync(int orderId)
        {
            string role = HttpContext.Session.GetString("account");
            if (role != null)
            {
                if (role.Equals("customer") || role.Equals("staff"))
                {
                    OrderDetail = _service.GetAllOrderDetailByOrderId(orderId);
                    return Page();
                }
            }
            return RedirectToPage("../Login");
        }
        public IActionResult OnPost()
        {
            string role = HttpContext.Session.GetString("account");
            if (role != null)
            {
                if (role.Equals("customer"))
                {
                    return RedirectToPage("../Orders/Index_Customer");
                }
                else if (role.Equals("seller"))
                {
                    return RedirectToPage("../Orders/Index_Seller");
                }
            }
            return RedirectToPage("../Login");
        }
    }
}
