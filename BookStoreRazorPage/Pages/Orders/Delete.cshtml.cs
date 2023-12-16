using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;
using System.Data;

namespace BookStoreRazorPage.Pages.Orders
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderService _service;

        public DeleteModel(OrderService orderService)
        {
            _service = orderService;
        }

        [BindProperty]
      public Order Order { get; set; } = default!;

        public IActionResult OnGetAsync(int id)
        {
            var role = HttpContext.Session.GetString("account");
            if(role.Equals("seller") || role.Equals("customer")) {
                if (id == null)
                {
                    return NotFound();
                }
                var order = _service.GetOrderById(id);
                if (order == null)
                {
                    return NotFound();
                }
                else
                {
                    Order = order;
                }
                return Page();
            }return RedirectToPage("../Logout");
        }

        public IActionResult OnPostAsync(int id)
        {
            if (id == null )
            {
                return NotFound();
            }
            var order = _service.GetOrderById(id);

            if (order != null)
            {
                Order = order;
                
                var orderDetails = _service.GetAllOrderDetailByOrderId(Order.Id);
                foreach (var orderDetail in orderDetails)
                {
                    _service.DeleteOrderDetail(orderDetail);
                }
                _service.DeleteOrder(Order);
                TempData["ResultSuccess"] = "Delete Successfully";
            }
            var role = HttpContext.Session.GetString("account");
            if (role.Equals("seller"))
            {
                return RedirectToPage("../Orders/IndexSeller");
            }
            else if (role.Equals("customer"))
            {
                return RedirectToPage("../Orders/IndexCustomer");
            }return RedirectToPage("../Logout");
        }
    }
}
