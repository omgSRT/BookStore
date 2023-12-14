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

namespace BookStoreRazorPage.Pages.CategoryPages
{
    public class EditModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly IOrderService _orderService;
        private readonly IBookService _bookService;
        public EditModel(ICategoryService categoryService, IOrderService orderService, IBookService bookService)
        {
            _categoryService = categoryService;
            _orderService = orderService;
            _bookService = bookService;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public IActionResult OnGet(int id)
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

            var category = _categoryService.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            Category = category;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var name = Category.Name;
                if (string.IsNullOrWhiteSpace(name))
                {
                    TempData["ErrorEditCategory"] = "name is required";
                    return Page();
                }
                var description = Category.Description;
                if (string.IsNullOrWhiteSpace(description))
                {
                    TempData["ErrorEditCategory"] = "description is required";
                    return Page();
                }
                if (Category.IsActive == false)
                {
                    var bookList = _bookService.GetAll().Where(x => x.CategoryId == Category.Id);
                    foreach (var book in bookList)
                    {
                        var bookListSell = _orderService.GetAllOrderDetails().Where(x => x.BookId == book.Id);
                        if (bookListSell.Any())
                        {
                            TempData["ErrorEditCategory"] = "Cannot deactivate category. Books of this category are associated with orders.";
                            return Page();
                        }
                    }
                    foreach (var book in bookList)
                    {
                        book.IsActive = false;
                        _bookService.Update(book);
                    }
                }
                _categoryService.Update(Category);


            }
            catch (DbUpdateConcurrencyException)
            {
                if (_categoryService.GetById(Category.Id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
