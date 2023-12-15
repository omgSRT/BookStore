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
    public class IndexModel : PageModel
    {
        private readonly IBookService _bookService;
        public IndexModel( IBookService bookService)
        {
            _bookService = bookService;
        }
        [BindProperty(SupportsGet = true)]
        public int curentPage { get; set; } = 1;
        public int pageSize { get; set; } = 5;
        public int count { get; set; }
        public int totalPages => (int)Math.Ceiling(Decimal.Divide(count, pageSize));

        public SelectList BookSelectList { get; set; }

        public IList<Book> Book { get; set; } = default!;

        public List<SelectListItem> IsActiveOptions { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "true", Text = "Active" },
            new SelectListItem { Value = "false", Text = "Inactive"}
        };

        private void UpdatePageData(string searchKeyword = null)
        {
            var books = _bookService.GetAll();
            BookSelectList = new SelectList(books, "Id", "Name");

            if (string.IsNullOrEmpty(searchKeyword))
            {
                count = _bookService.GetAllWithIncludeCategoryAndPublisher().Count();
                Book = _bookService.GetAllWithIncludeCategoryAndPublisher().Skip((curentPage - 1) * pageSize).Take(pageSize)
                    .ToList(); ;
            }
            else
            {
                count = _bookService.GetByName(searchKeyword).Count();
                Book = _bookService.GetByName(searchKeyword).Skip((curentPage - 1) * pageSize).Take(pageSize)
                    .ToList();
            }
        }

        public IActionResult OnGet()
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
            UpdatePageData();

            return Page();
        }


        public IActionResult OnPostEditAccount(int id)
        {
            var isActive = HttpContext.Request.Form["IsActive"];
            var storeId = int.Parse(HttpContext.Request.Form["StoreId"]);
            var bookToUpdate = _bookService.GetById(id);
            bookToUpdate.IsActive = isActive == "true" ? true : false;
            _bookService.Update(bookToUpdate);
            HttpContext.Session.SetString("UpdateSuccessMessage", "Account updated successfully!");

            return RedirectToPage("./Index");
        }
        public IActionResult OnPost()
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

            string searchKeyword = Request.Form["search"];
            UpdatePageData(searchKeyword);
            return Page();
        }
    }
}
