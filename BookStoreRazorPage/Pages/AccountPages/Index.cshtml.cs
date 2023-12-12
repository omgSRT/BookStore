using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;

namespace BookStoreRazorPage.Pages.AccountPages
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;

        public IndexModel()
        {
            _accountService = new AccountService();
        }

        public IList<Account> Account { get;set; } = default!;

        public IActionResult OnGet()
        {
            var loginSession = HttpContext.Session.GetString("account");
            if(loginSession == null)
            {
                TempData["ErrorLogin"] = "You need to login to access this page";
                return RedirectToPage("../Login");
            }
            else if (!loginSession.Equals("admin"))
            {
                TempData["ErrorAuthorize"] = "You don't have permission to access this page";
                return RedirectToPage("../Error");
            }
            Account = _accountService.GetAllWithIncludeRoleAndStore();
            return Page();
        }
    }
}
