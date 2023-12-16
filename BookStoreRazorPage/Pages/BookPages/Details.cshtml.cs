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

namespace BookStoreRazorPage.Pages.BookPages
{
    public class DetailsModel : PageModel
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IPublisherService _publisherService;

        public DetailsModel(IBookService bookService, ICategoryService categoryService, IPublisherService publisherService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _publisherService = publisherService;
        }

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
                else if (!loginSession.Equals("admin"))
                {
                    TempData["ErrorAuthorize"] = "You don't have permission to access this page";
                    return RedirectToPage("../Error");
                }

                if (id == null)
                {
                    return NotFound();
                }

                var book = _bookService.GetAllWithIncludeCategoryAndPublisher().Where(x => x.Id == (int)id).FirstOrDefault();
                if (book == null)
                {
                    return NotFound();
                }
                else
                {
                    ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "Id", "Name");
                    ViewData["PublisherId"] = new SelectList(_publisherService.GetAll(), "Id", "Name");
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
