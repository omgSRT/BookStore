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

namespace BookStoreRazorPage.Pages.BookInStorePages
{
    public class EditModel : PageModel
    {
        private readonly IBookInStoreService _bookInStoreService;
        private readonly IBookService _bookService;
        private readonly IAccountService _accountService;

        public EditModel(IBookInStoreService bookInStoreService, IBookService bookService, IAccountService accountService)
        {
            _bookInStoreService = bookInStoreService;
            _bookService = bookService;
            _accountService = accountService;
        }

        [BindProperty]
        public BookInStore BookInStore { get; set; } = default!;
        [BindProperty]
        public int StoreId { get; set; }

        public IActionResult OnGet(int? id)
        {
            try
            {
                var loginSession = HttpContext.Session.GetString("account");
                if (loginSession == null)
                {
                    TempData["ErrorLogin"] = "You need to login to access";
                    return RedirectToPage("../Login");
                }
                else if (!loginSession.Equals("seller"))
                {
                    TempData["ErrorAuthorize"] = "You don't have permission to access this page";
                    return RedirectToPage("../Error");
                }
                if (id == null)
                {
                    return NotFound();
                }

                var bookinstore = _bookInStoreService.GetById((int)id);
                if (bookinstore == null)
                {
                    return NotFound();
                }
                var accid = HttpContext.Session.GetInt32("accountId");
                var account = _accountService.GetById((int)accid);

                BookInStore = bookinstore;
                StoreId = (int)account.StoreId;
                ViewData["BookId"] = new SelectList(_bookService.GetAll(), "Id", "Name");
                return Page();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error Occurred. Please contact admin";
                Console.WriteLine(ex.ToString());
                return RedirectToPage("../Error");
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updateBookInStore = _bookInStoreService.GetById(BookInStore.Id);

                    var book = _bookService.GetById((int)BookInStore.BookId!);
                    if (BookInStore.Amount > book.Amount)
                    {
                        ViewData["BookId"] = new SelectList(_bookService.GetAll(), "Id", "Name");

                        TempData["Error"] = "There is/are only " + book.Amount + " books left";
                        return Page();
                    }

                    updateBookInStore!.Amount = BookInStore.Amount;
                    _bookInStoreService.Update(updateBookInStore!);
                    var importAmount = book.Amount - BookInStore.Amount;
                    book.Amount = importAmount;
                    _bookService.Update(book);

                    TempData["ResultSuccess"] = "Update Successfully";
                    return RedirectToPage("./Index");
                }

                TempData["ResultFailed"] = "Error Occurred. Cannot Update";
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
