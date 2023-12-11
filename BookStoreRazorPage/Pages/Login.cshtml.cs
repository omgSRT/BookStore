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
    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;
        private readonly IStoreService _storeService;

        public LoginModel()
        {
            _accountService = new AccountService();
            _roleService = new RoleService();
            _storeService = new StoreService();
        }

        public IActionResult OnGet()
        {
            try
            {
                ViewData["RoleId"] = new SelectList(_roleService.GetAll(), "Id", "Name");
                ViewData["StoreId"] = new SelectList(_storeService.GetAll(), "Id", "Name");
                return Page();
            }
            catch(Exception ex)
            {
                TempData["ErrorLogin"] = "An Error Occurred. Please notify admin.";
                Console.WriteLine($"Error occurred: {ex.Message}");
                return Page();
            }
        }

        [BindProperty]
        public Account Account { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        { 
            try
            {
                var username = Account.Username;
                var password = Account.Password;
                if(username == null || password == null)
                {
                    TempData["ErrorLogin"] = "You must type username and password to login";
                }
                else
                {
                    IConfiguration config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
                    var adminUsername = config["Admin:Username"];
                    var adminPassword = config["Admin:Password"];
                    if(username.Equals(adminUsername) && password.Equals(adminPassword))
                    {
                        HttpContext.Session.SetString("account", "admin");
                        return Page();
                    }
                    else
                    {
                        var account = _accountService.GetByUsernameAndPassword(username, password);
                        if(account == null)
                        {
                            TempData["ErrorLogin"] = "Wrong email or password";
                            return Page();
                        }
                        else
                        {
                            if(account.IsActive == false) {
                                TempData["ErrorLogin"] = "Wrong email or password";
                                return Page();
                            }
                            else if(account.IsActive == true && account.RoleId == 1) {
                                HttpContext.Session.SetString("account", "customer");
                                return RedirectToPage("./AccountPages/Edit", new { id = account.Id });
                            }
                            else if (account.IsActive == true && account.RoleId == 2)
                            {
                                HttpContext.Session.SetString("account", "staff");
                                return Page();
                            }
                        }
                    }
                }

                TempData["ErrorLogin"] = "Login Failed. Please contact admin to fix the problem";
                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorLogin"] = "An Error Occurred. Please notify admin.";
                Console.WriteLine($"Error occurred: {ex.Message}");
                return Page();
            }
        }
    }
}
