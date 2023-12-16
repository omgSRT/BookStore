using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;

namespace BookStoreRazorPage.Pages.BookInStorePages
{
    public class DeleteModel : PageModel
    {
        private readonly IBookInStoreService _bookInStoreService;
        private readonly IOrderService _orderService;
        private readonly IBookService _bookService;

        public DeleteModel(IBookInStoreService bookInStoreService, IOrderService orderService, IBookService bookService)
        {
            _bookInStoreService = bookInStoreService;
            _orderService = orderService;
            _bookService = bookService;
        }

        [BindProperty]
      public BookInStore BookInStore { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            try
            {
                var loginSession = HttpContext.Session.GetString("account");
                if (loginSession == null)
                {
                    TempData["ErrorLogin"] = "You need to login to access this page";
                    return RedirectToPage("../Login");
                }
                if (id == null)
                {
                    return NotFound();
                }

                var bookinstore = _bookInStoreService.GetAllWithIncludeBookAndStore()
                    .Where(x => x.Id == (int)id).FirstOrDefault();

                if (bookinstore == null)
                {
                    return NotFound();
                }
                else
                {
                    BookInStore = bookinstore;
                }
                return Page();
            }
            catch (Exception ex) {
                TempData["Error"] = "Error Occurred. Please contact admin";
                Console.WriteLine(ex.ToString());
                return RedirectToPage("../Error");
            }
        }

        public IActionResult OnPost(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var bookinstore = _bookInStoreService.GetById((int)id);

                if(bookinstore != null)
                { 
                    var orderDetailList = _orderService.GetAllOrderDetails().Where(x => x.BookInStoreId == (int)id).ToList();
                    if (orderDetailList.Any())
                    {
                        IList<Order> orderList = new List<Order>();
                        foreach (var orderDetail in orderDetailList)
                        {
                            var order = _orderService.GetOrderById((int)orderDetail.OrderId!);
                            if(order.OrderStatus!.Equals("Processing", StringComparison.OrdinalIgnoreCase))
                            {
                                orderList.Add(order);
                            }
                        }
                        if (orderDetailList.Any())
                        {
                            TempData["ResultFailed"] = "There's processing order with this book. Cannot Delete";
                            return RedirectToPage("./Index");
                        }
                    }
                    if(bookinstore.Amount > 0)
                    {
                        var book = _bookService.GetById((int)bookinstore.BookId);
                        book.Amount += bookinstore.Amount;
                        _bookService.Update(book);
                    }


                    _bookInStoreService.Delete(bookinstore);

                    TempData["ResultSuccess"] = "Delete Successfully";
                    return RedirectToPage("./Index");
                }

                TempData["ResultFailed"] = "Error Occurred. Cannot Delete";
                return RedirectToPage("./Index");
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
