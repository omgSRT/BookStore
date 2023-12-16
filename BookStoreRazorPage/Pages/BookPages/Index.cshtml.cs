﻿using System;
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
        public IndexModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        [BindProperty]
        public string SearchValue { get; set; } = default!;

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


        public IActionResult OnGet()
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

                count = _bookService.GetAll().Count();
                Book = _bookService.GetAll()
                    .Skip((curentPage - 1) * pageSize).Take(pageSize)
                    .ToList();
                return Page();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error Occurred. Please contact admin";
                Console.WriteLine(ex.ToString());
                return RedirectToPage("../Error");
            }
        }

        public IActionResult OnPost()
        {
            if (SearchValue is null)
            {
                count = _bookService.GetAll().Count();
                Book = _bookService.GetAll()
                    .Skip((curentPage - 1) * pageSize).Take(pageSize)
                    .ToList();
                return Page();
            }
            else if (SearchValue.Trim().Length == 0)
            {
                count = _bookService.GetAll().Count();
                Book = _bookService.GetAll()
                    .Skip((curentPage - 1) * pageSize).Take(pageSize)
                    .ToList();
                return Page();
            }
            else
            {
                count = _bookService.GetAll()
                    .Where(x => x.Name.Contains(SearchValue, StringComparison.OrdinalIgnoreCase))
                    .Count();
                Book = _bookService.GetAll()
                    .Where(x => x.Name.Contains(SearchValue, StringComparison.OrdinalIgnoreCase))
                    .Skip((curentPage - 1) * pageSize).Take(pageSize)
                    .ToList();
                if (Book.Count == 0)
                {
                    TempData["ResultFailed"] = "There's no result matched " + SearchValue;
                    count = _bookService.GetAll().Count();
                    Book = _bookService.GetAll()
                        .Skip((curentPage - 1) * pageSize).Take(pageSize)
                        .ToList();
                }
                return Page();
            }
        }
    }
}
