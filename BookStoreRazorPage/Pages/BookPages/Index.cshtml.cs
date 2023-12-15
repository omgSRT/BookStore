using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;

namespace BookStoreRazorPage.Pages.BookPages
{
    public class IndexModel : PageModel
    {
        private readonly IBookService _bookService;
        private readonly IAccountService _accountService;

        public IndexModel()
        {
            _bookService = new BookService();
            _accountService = new AccountService();
        }

        public IList<Book> Book { get;set; } = default!;

        public IActionResult OnGet()
        {
            try
            {
                var loginSession = HttpContext.Session.GetString("account");
                var id = HttpContext.Session.GetInt32("accountId");
                if (loginSession == null)
                {
                    TempData["ErrorLogin"] = "You need to login to access this page";
                    return RedirectToPage("../Login");
                }

                var account = _accountService.GetById((int)id);

                var list = _bookService.GetAll();
                if (list != null)
                {
                    Book = list;
                }
                return Page();
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
