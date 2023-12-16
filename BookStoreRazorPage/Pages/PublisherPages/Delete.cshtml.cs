using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;

namespace BookStoreRazorPage.Pages.PublisherPages
{
    public class DeleteModel : PageModel
    {
        private readonly IPublisherService _publisherService;
        private readonly IBookService _bookService;

        public DeleteModel(PublisherService publisherService, BookService bookService)
        {
            _publisherService = publisherService;
            _bookService = bookService;
        }

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;

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

                var publisher = _publisherService.GetById((int)id);

                if (publisher == null)
                {
                    return NotFound();
                }
                else
                {
                    Publisher = publisher;
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
                var publisher = _publisherService.GetById((int)id);

                if (publisher != null)
                {
                    var bookList = _bookService.GetAll().Where(x => x.PublisherId == (int)id && x.IsActive == true);
                    if(bookList.Any())
                    {
                        TempData["ResultFailed"] = "There are selling books with this publisher. Cannot delete";
                        return RedirectToPage("./Index");
                    }

                    Publisher = publisher;
                    Publisher.IsActive = false;
                    _publisherService.Update(Publisher);

                    TempData["ResultSuccess"] = "Delete Successfully";
                    return RedirectToPage("./Index");
                }

                TempData["ResultFailed"] = "Error Occurred. Cannot Delete";
                return RedirectToPage("./Index");
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Error Occurred. Please contact admin";
                Console.WriteLine(ex.ToString());
                return RedirectToPage("../Error");
            }
        }
    }
}
