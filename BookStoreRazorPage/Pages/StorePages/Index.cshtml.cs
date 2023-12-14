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
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.BookStoreDBContext _context;

        public IndexModel(BusinessObject.BookStoreDBContext context)
        {
            _context = context;
        }

        public IList<Store> Store { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Stores != null)
            {
                Store = await _context.Stores.ToListAsync();
            }
        }
    }
}
