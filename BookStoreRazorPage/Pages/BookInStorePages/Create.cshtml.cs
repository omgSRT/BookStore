using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Service;

namespace BookStoreRazorPage.Pages.BookInStorePages
{
    public class CreateModel : PageModel
    {
        private readonly IBookInStoreService _bookInStoreService;
        private readonly IBookService _bookService;
        private readonly IAccountService _accountService;

        public CreateModel()
        {
            _bookInStoreService = new BookInStoreService();
            _bookService = new BookService();
            _accountService = new AccountService();
        }

        [BindProperty]
        public BookInStore BookInStore { get; set; } = default!;

        public IActionResult OnGet()
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
                else
                {
                    var id = HttpContext.Session.GetInt32("accountId");
                    var account = _accountService.GetById((int)id);
                    List<int?> excludedBookIds = _bookInStoreService.GetAll()
                        .Where(bis => bis.BookId.HasValue)
                        .Select(bis => bis.BookId)
                        .ToList();
                    List<Book> filteredBookList = _bookService.GetAll()
                            .Where(book => !excludedBookIds.Contains(book.Id))
                            .ToList();
                    ViewData["BookId"] = new SelectList(filteredBookList, "Id", "Name");
                }
                return Page();
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Error Occurred. Please contact admin";
                Console.WriteLine(ex.ToString());
                return RedirectToPage("../Error");
            }
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(BookInStore.BookId == 0)
                    {
                        var id = HttpContext.Session.GetInt32("accountId");
                        var account = _accountService.GetById((int)id);
                        List<int?> excludedBookIds = _bookInStoreService.GetAll()
                            .Where(bis => bis.BookId.HasValue)
                            .Select(bis => bis.BookId)
                            .ToList();
                        List<Book> filteredBookList = _bookService.GetAll()
                                .Where(book => !excludedBookIds.Contains(book.Id))
                                .ToList();
                        ViewData["BookId"] = new SelectList(filteredBookList, "Id", "Name");

                        TempData["Error"] = "Please select books to add";
                        return Page();
                    }
                    var book = _bookService.GetById((int)BookInStore.BookId!);
                    if(BookInStore.Amount > book.Amount)
                    {
                        List<int?> excludedBookIds = _bookInStoreService.GetAll()
                            .Where(bis => bis.BookId.HasValue)
                            .Select(bis => bis.BookId)
                            .ToList();
                        List<Book> filteredBookList = _bookService.GetAll()
                                .Where(book => !excludedBookIds.Contains(book.Id))
                                .ToList();
                        ViewData["BookId"] = new SelectList(filteredBookList, "Id", "Name");

                        TempData["Error"] = "There is/are only " +book.Amount+ " books left";
                        return Page();
                    }

                    _bookInStoreService.Add(BookInStore);
                    var importAmount = book.Amount - BookInStore.Amount;
                    book.Amount = importAmount;
                    _bookService.Update(book);

                    TempData["ResultSuccess"] = "Create Successfully";
                    return RedirectToPage("./Index");
                }

                TempData["ResultFailed"] = "Error Occurred. Cannot Create";
                return RedirectToPage("./Index");
            }
            catch (Exception ex) {
                TempData["Error"] = "Error Occurred. Please contact admin";
                Console.WriteLine(ex.ToString());
                return RedirectToPage("../Error");
            }
        }
    }
}
