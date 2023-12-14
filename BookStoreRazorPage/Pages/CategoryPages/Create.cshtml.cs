using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using Service;

namespace BookStoreRazorPage.Pages.CategoryPages
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public CreateModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;
        public IActionResult OnGet()
        {
            var loginSession = HttpContext.Session.GetString("account");
            if (loginSession == null)
            {
                TempData["ErrorLogin"] = "You need to login to access this page";
                return RedirectToPage("../Login");
            }
            else if (!loginSession.Equals("admin") && !loginSession.Equals("customer") && !loginSession.Equals("seller"))
            {
                TempData["ErrorAuthorize"] = "You don't have permission to access this page";
                return RedirectToPage("../Error");
            }
            return Page();
        }

        


       
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid )
            {
                return Page();
            }

            var name = Category.Name;
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["ErrorCreateCategory"] = "name is required";
                return Page();
            }
            var description = Category.Description;
            if (string.IsNullOrWhiteSpace(description))
            {
                TempData["ErrorCreateCategory"] = "description is required";
                return Page();
            }
            _categoryService.Add(Category);

            return RedirectToPage("./Index");
        }
    }
}
