﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;

namespace BookStoreRazorPage.Pages.StorePages
{
    public class EditModel : PageModel
    {
        private readonly BusinessObject.BookStoreDBContext _context;

        public EditModel(BusinessObject.BookStoreDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Store Store { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store =  await _context.Stores.FirstOrDefaultAsync(m => m.Id == id);
            if (store == null)
            {
                return NotFound();
            }
            Store = store;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Store).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(Store.Id))
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

        private bool StoreExists(int id)
        {
          return (_context.Stores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
