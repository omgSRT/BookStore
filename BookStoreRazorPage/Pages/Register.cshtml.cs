using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using Service;

namespace BookStoreRazorPage.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountService _accountService;

        public RegisterModel()
        {
            _accountService = new AccountService();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPostAsync()
        {
            try
            {
                var existedAccountByUsername = _accountService.GetAll()
                    .Where(acc => acc.Username!.Equals(Account.Username, StringComparison.OrdinalIgnoreCase));
                var username = Account.Username;
                var password = Account.Password;
                var chosenDate = Account.DateOfBirth!;
                TimeSpan timeSpan = (TimeSpan)(DateTime.Now - chosenDate);
                if (existedAccountByUsername.Any())
                {
                    TempData["ErrorRegister"] = "Username already existed";
                    return Page();
                }
                if (username!.Trim().Equals(string.Empty) || password!.Trim().Equals(string.Empty))
                {
                    TempData["ErrorRegister"] = "Username and Password are required";
                    return Page();
                }
                if(timeSpan.TotalDays < 10 * 365.25)
                {
                    TempData["ErrorRegister"] = "You must be at least 10 years old to register";
                    return Page();
                }

                Account newAccount = new Account
                {
                    Username = Account.Username,
                    Password = Account.Password,
                    Name = Account.Name,
                    Phone = Account.Phone,
                    DateOfBirth = Account.DateOfBirth,
                    IsActive = true,
                    RoleId = 2,
                    Address = Account.Address,
                };

                _accountService.Add(newAccount);
                TempData["SuccessRegister"] = "Register Successfully";
                return RedirectToPage("Login");
            }
            catch (Exception ex)
            {
                TempData["ErrorRegister"] = "Some Error Occurred. Please contact admin to fix";
                Console.WriteLine($"Error Occurred: {ex.Message}");
                return Page();
            }
        }
    }
}
