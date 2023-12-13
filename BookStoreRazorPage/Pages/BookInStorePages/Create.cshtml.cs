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

namespace BookStoreRazorPage.Pages.BookInStorePages
{
    public class CreateModel : PageModel
    {
        private readonly IBookInStoreRepository _bookInStoreRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IAccountRepository _accountRepository;

        public CreateModel()
        {
            _bookInStoreRepository = new BookInStoreRepository();
            _bookRepository = new BookRepository();
            _accountRepository = new AccountRepository();
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
                    var account = _accountRepository.GetById((int)id);
                    List<int?> excludedBookIds = _bookInStoreRepository.GetAll()
                        .Where(bis => bis.BookId.HasValue)
                        .Select(bis => bis.BookId)
                        .ToList();
                    List<Book> filteredBookList = _bookRepository.GetAll()
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
                    _bookInStoreRepository.Add(BookInStore);

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
