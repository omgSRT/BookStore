using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace BookStoreRazorPage.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly IBookInStoreService _bookInStoreService;
        private readonly IAccountService _accountService;
        private readonly IBookService _bookService;
        private readonly IOrderService _orderService;


        public CreateModel()
        {
            _bookInStoreService = new BookInStoreService();
            _accountService = new AccountService();
            _bookService = new BookService();
            _orderService = new OrderService();
        }

        public IList<BookInStore> BookInStore { get;set; } = default!;
        public IList<Store> StoreList { get;set; }
        public int StoreId { get;set; }
        public int Quantity { get;set; }
        private IList<OrderDetail> Card{ get;set; } = default!;


        public IActionResult OnGetAsync()
        {
            string role = HttpContext.Session.GetString("account");
            if (role != null)
            {
                if (role.Equals("customer"))
                {
                    BookInStore = _bookInStoreService.GetAllWithIncludeBookAndStore();
                    return Page();
                }
            }
            return RedirectToPage("../Logout");
            
        }
        public async Task<IActionResult> OnPostAsync(int _BookInStoreId, int _BookId)
        {

            var id = (int)HttpContext.Session.GetInt32("accountId");
            var address = _accountService.GetById(id).Address;
            var price = _bookService.GetById(_BookId).Price;

            var oder = new Order()
            {
                TotalPrice = Quantity * price,
                ShippingAddress = address,
                CustomerId = id,
                OrderStatus ="Processing",
                CreateDate = DateTime.Now
            };
            _orderService.AddOrder(oder);

            var orderDetail = new OrderDetail()
            {
                OrderId = oder.Id,
                BookInStoreId = _BookInStoreId,
                BookId = _BookId,
                Quantity = Quantity,
            };
            _orderService.AddOrderDetail(orderDetail);

            var updateBookInStore = _bookInStoreService.GetById(_BookInStoreId);
            updateBookInStore.Amount -= Quantity;
            _bookInStoreService.Update(updateBookInStore);
            return RedirectToPage("/Orders/IndexCustomer");
        }
        
    }
}
