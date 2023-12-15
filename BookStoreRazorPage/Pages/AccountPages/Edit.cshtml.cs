using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;

namespace BookStoreRazorPage.Pages.AccountPages
{
    public class EditModel : PageModel
    {
        private readonly IAccountService _accountService;

        public EditModel()
        {
            _accountService = new AccountService();
        }


        [BindProperty]
        public Account Account { get; set; } = default!;

        [BindProperty]
        public IFormFile AvatarFile { get; set; }

        public IActionResult OnGet(int id)
        {
            var loginSession = HttpContext.Session.GetString("account");
            if (loginSession == null)
            {
                TempData["ErrorLogin"] = "You need to login to access this page";
                return RedirectToPage("../Login");
            }
            else if (!loginSession.Equals("customer") && !loginSession.Equals("seller"))
            {
                TempData["ErrorAuthorize"] = "You don't have permission to access this page";
                return RedirectToPage("../Error");
            }
            Account = _accountService.GetById(id);
            if (Account == null)
            {
                return NotFound();
            }
            return Page();
        }


        public IActionResult OnPost()
        {
            var chosenDate = Account.DateOfBirth!;
            TimeSpan timeSpan = (TimeSpan)(DateTime.Now - chosenDate);
            if (timeSpan.TotalDays < 10 * 365.25)
            {
                TempData["ErrorEditProfile"] = "You must be at least 10 years old to register";
                return Page();
            }
            // Kiểm tra password
            var password = Account.Password;
            if (string.IsNullOrWhiteSpace(password))
            {
                TempData["ErrorEditProfile"] = "Password is required";
                return Page();
            }

            // Kiểm tra address
            var address = Account.Address;
            if (string.IsNullOrWhiteSpace(address))
            {
                TempData["ErrorEditProfile"] = "Address is required";
                return Page();
            }

            if (AvatarFile != null && AvatarFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    AvatarFile.CopyTo(memoryStream);
                    Account.Avatar = Convert.ToBase64String(memoryStream.ToArray());
                }
            }


            _accountService.Update(Account); 
            return RedirectToPage("./Details", new { id = Account.Id });
        }
    }
}
