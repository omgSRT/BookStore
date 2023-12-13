﻿using System;
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
    public class DetailsModel : PageModel
    {
        private readonly IAccountService _accountService;

        public DetailsModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

      public Account Account { get; set; } = default!; 

        public IActionResult OnGetAsync(int? id)
        {
            var loginSession = HttpContext.Session.GetString("account");
            if (loginSession == null)
            {
                TempData["ErrorLogin"] = "You need to login to access this page";
                return RedirectToPage("../Login");
            }
            else if (!loginSession.Equals("admin") && !loginSession.Equals("customer") && !loginSession.Equals("seller"))
            {
                TempData["ErrorAuthorize"] = "You don't have permission to access this page";
                return RedirectToPage("../Error");
            }
            if (id == null )
            {
                return NotFound();
            }

            var account = _accountService.GetById(id.Value);
            if (account == null)
            {
                return NotFound();
            }
            else 
            {
                Account = account;
            }
            return Page();
        }
    }
}
