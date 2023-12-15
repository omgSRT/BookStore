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
        private readonly IBookInStoreService _bookInStoreService;

        public EditModel()
        {
            _orderService = new OrderService();
            _bookInStoreService= new BookInStoreService();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;
        [BindProperty]
        public string Mess { get;set; }
        [BindProperty]
        public string BookName { get; set; }
        private int? currentQuantity { get; set; }

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
            currentQuantity = OrderDetail.Quantity;
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
                var updateBookInStore = _bookInStoreService.GetById((int)OrderDetail.BookInStoreId);
                updateBookInStore.Amount += (currentQuantity - OrderDetail.Quantity);
                _bookInStoreService.Update(updateBookInStore);

                TempData["ResultSuccess"] = "Update Successfully";
                return RedirectToPage("../OrderDetails/Index", new { orderId = OrderDetail.OrderId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error Occurred. Please contact admin";
                Console.WriteLine(ex.ToString());
                return RedirectToPage("../Error");
            }
        }
    }
}
