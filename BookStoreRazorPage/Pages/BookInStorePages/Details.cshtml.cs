﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Repository;

namespace BookStoreRazorPage.Pages.BookInStorePages
{
    public class DetailsModel : PageModel
    {
        private readonly IBookInStoreRepository _bookInStoreRepository;

        public DetailsModel()
        {
            _bookInStoreRepository = new BookInStoreRepository();
        }

      public BookInStore BookInStore { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var bookinstore = _bookInStoreRepository.GetAllWithIncludeBookAndStore()
                    .Where(x => x.Id == (int)id).FirstOrDefault();
                if (bookinstore == null)
                {
                    return NotFound();
                }
                else
                {
                    BookInStore = bookinstore;
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