using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;

namespace BookStoreRazorPage.Pages.StorePages
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObject.BookStoreDBContext _context;

        public DetailsModel(BusinessObject.BookStoreDBContext context)
        {
            _context = context;
        }

      public Store Store { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores.FirstOrDefaultAsync(m => m.Id == id);
            if (store == null)
            {
                return NotFound();
            }
            else 
            {
                Store = store;
            }
            return Page();
        }
    }
}
