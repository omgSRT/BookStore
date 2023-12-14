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

namespace BookStoreRazorPage.Pages.BookPages
{
    public class EditModel : PageModel
    {
        private readonly IBookService _bookService;

        public EditModel()
        {
            _bookService = new BookService();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
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

                var book = _bookService.GetById((int)id);
                if (book == null)
                {
                    return NotFound();
                }
                Book = book;
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
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updateBook = _bookService.GetById(Book.Id);
                    updateBook!.Amount = Book.Amount;
                    _bookService.Update(updateBook!);

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
