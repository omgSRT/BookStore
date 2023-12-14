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
    public class DetailsModel : PageModel
    {
        private readonly IBookService _bookService;

        public DetailsModel()
        {
            _bookService = new BookService();
        }

      public Book Book { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
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
    }
}
