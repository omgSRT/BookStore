using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;
using System.Data;

namespace BookStoreRazorPage.Pages.OrderDetails
{
    public class EditModel : PageModel
    {
        private readonly IOrderService _orderService;

        public EditModel()
        {
            _orderService = new OrderService();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;
        [BindProperty]
        public string Mess { get;set; }
        [BindProperty]
        public string BookName { get; set; }

        public IActionResult OnGetAsync(int id, string bookname)
        {
            
            if (id == null )
            {
                return NotFound();
            }

            var orderdetail = _orderService.GetOrderDetailById(id);
            if(orderdetail == null)
            {
                return NotFound();
            }

            OrderDetail = orderdetail;
            BookName = bookname;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                ModelState.Clear();
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                _orderService.UpdateOrderDetail(OrderDetail);
                var role = HttpContext.Session.GetString("account");
                if (role.Equals("seller"))
                {
                    return RedirectToPage("../Orders/IndexSeller", new { orderId = OrderDetail.OrderId });
                }
                else if (role.Equals("customer"))
                {
                    return RedirectToPage("../Orders/IndexCustomer", new { orderId = OrderDetail.OrderId });
                }
                return RedirectToPage("Logout");
            }
            catch (Exception ex)
            {
                Mess = ex.Message;
                return Page();
            }
        }
    }
}
