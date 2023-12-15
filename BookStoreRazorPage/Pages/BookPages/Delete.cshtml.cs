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
    public class DeleteModel : PageModel
    {
        private readonly IBookService _bookService;

        public DeleteModel()
        {
            _bookService = new BookService();
        }

        [BindProperty]
      public Book Book { get; set; } = default!;

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

                var book = _bookService.GetAll()
                    .Where(x => x.Id == (int)id).FirstOrDefault();

                if (book == null)
                {
                    return NotFound();
                }
                else
                {
                    Book = book;
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
        public IActionResult OnPost(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var book = _bookService.GetById((int)id);

                if (book != null)
                {
                    _bookService.Delete(book);

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
