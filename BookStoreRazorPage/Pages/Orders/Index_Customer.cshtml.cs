﻿using System;
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
    public class Index_CustomerModel : PageModel
    {
        private readonly IOrderService _service;

        public Index_CustomerModel()
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
                if (role.Equals("customer"))
                {
                    Order = _service.GetOrderByCustomerID(id);
                    return Page();
                }
            }return RedirectToPage("../Logout");
        }
    }
}
