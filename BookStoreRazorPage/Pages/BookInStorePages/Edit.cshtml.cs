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

namespace BookStoreRazorPage.Pages.BookInStorePages
{
    public class EditModel : PageModel
    {
        private readonly IBookInStoreService _bookInStoreService;
        private readonly IBookService _bookService;

        public EditModel()
        {
            _bookInStoreService = new BookInStoreService();
            _bookService = new BookService();
        }

        [BindProperty]
        public BookInStore BookInStore { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
                var loginSession = HttpContext.Session.GetString("account");
                if (loginSession == null)
                {
                    TempData["ErrorLogin"] = "You need to login to access";
                    return RedirectToPage("../Login");
                }
                else if (!loginSession.Equals("seller"))
                {
                    TempData["ErrorAuthorize"] = "You don't have permission to access this page";
                    return RedirectToPage("../Error");
                }
                if (id == null)
                {
                    return NotFound();
                }

                var bookinstore = _bookInStoreService.GetById((int)id);
                if (bookinstore == null)
                {
                    return NotFound();
                }
                BookInStore = bookinstore;
                ViewData["BookId"] = new SelectList(_bookService.GetAll(), "Id", "Name");
                return Page();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error Occurred. Please contact admin";
                Console.WriteLine(ex.ToString());
                return RedirectToPage("../Error");
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updateBookInStore = _bookInStoreService.GetById(BookInStore.Id);
                    updateBookInStore!.Amount = BookInStore.Amount;
                    _bookInStoreService.Update(updateBookInStore!);

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
