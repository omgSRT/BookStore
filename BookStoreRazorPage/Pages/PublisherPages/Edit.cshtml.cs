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
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;

namespace BookStoreRazorPage.Pages.PublisherPages
{
    public class EditModel : PageModel
    {
        private readonly IPublisherService _publisherService;
        private readonly IBookService _bookService;

        public EditModel(PublisherService publisherService, BookService bookService)
        {
            _publisherService = publisherService;
            _bookService = bookService;
        }

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;

        public IActionResult OnGet(int? id)
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

            ViewData["IsActive"] = new SelectList(new[]
                    {
                    new { Value = true, Text = "Active" },
                    new { Value = false, Text = "Inactive" }
                }, "Value", "Text");

            var publisher = _publisherService.GetById((int)id);
            if (publisher == null)
            {
                return NotFound();
            }
            Publisher = publisher;

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
                    int id = Publisher.Id;
                    bool isActive = (bool)Publisher.IsActive!;
                    var bookList = _bookService.GetAll().Where(x => x.PublisherId == id && x.IsActive == true);
                    if (bookList.Any() && isActive == false)
                    {
                        TempData["ResultFailed"] = "There are selling books with this publisher. Cannot update";
                        return RedirectToPage("./Index");
                    }

                    _publisherService.Update(Publisher);

                    TempData["ResultSuccess"] = "Update Successfully";
                    return RedirectToPage("./Index");
                }


                TempData["ResultFailed"] = "Error occurred while updating. Cannot update";
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
