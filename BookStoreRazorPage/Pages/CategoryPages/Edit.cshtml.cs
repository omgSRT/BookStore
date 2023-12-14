﻿using System;
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

        public EditModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
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
                _categoryService.Update(Category);
                if(Category.IsActive == false)
                {

                }
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
