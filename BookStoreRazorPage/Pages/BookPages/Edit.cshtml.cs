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
        private readonly ICategoryService _categoryService;
        private readonly IPublisherService _publisherService;

        public EditModel(IBookService bookService, ICategoryService categoryService, IPublisherService publisherService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _publisherService = publisherService;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            var loginSession = HttpContext.Session.GetString("account");
            if (loginSession == null)
            {
                TempData["ErrorLogin"] = "You need to login to access this page";
                return RedirectToPage("../Login");
            }
            else if (!loginSession.Equals("admin")|| !loginSession.Equals("seller"))
            {
                TempData["ErrorAuthorize"] = "You don't have permission to access this page";
                return RedirectToPage("../Error");
            }

            var book = _bookService.GetById((int)id);

            if (book == null)
            {
                return NotFound();
            }

            Book = book;
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "Id", "Name");
            ViewData["PublisherId"] = new SelectList(_publisherService.GetAll(), "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updateBook = _bookService.GetById(Book.Id);
                    var priceBook = Book.Price;
                    if(priceBook == null) {
                        TempData["ResultFailed"] = "Please input price";
                        return RedirectToPage("./Index");
                    }
                    if(priceBook <= 0)
                    {
                        TempData["ResultFailed"] = "Price must be more than 0";
                        return RedirectToPage("./Index");
                    }
                    _bookService.Update(Book);
                    ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "Id", "Name");
                    ViewData["PublisherId"] = new SelectList(_publisherService.GetAll(), "Id", "Name");

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
