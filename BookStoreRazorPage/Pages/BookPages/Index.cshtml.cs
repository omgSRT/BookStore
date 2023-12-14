using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;

namespace BookStoreRazorPage.Pages.BookPages
{
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.BookStoreDBContext _context;

        public IndexModel(BusinessObject.BookStoreDBContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Books != null)
            {
                Book = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher).ToListAsync();
            }
        }
    }
}
