using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;

namespace BookStoreRazorPage.Pages.AccountPages
{
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.BookStoreDBContext _context;

        public IndexModel(BusinessObject.BookStoreDBContext context)
        {
            _context = context;
        }

        public IList<Account> Account { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Accounts != null)
            {
                Account = await _context.Accounts
                .Include(a => a.Role)
                .Include(a => a.Store).ToListAsync();
            }
        }
    }
}
