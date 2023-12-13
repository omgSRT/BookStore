using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography.X509Certificates;

namespace BookStoreRazorPage.Pages.AccountPages
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;
        private readonly IStoreService _storeService;
        public IndexModel(IRoleService roleService, IAccountService accountService, IStoreService storeService)
        {
            _accountService = accountService;
            _roleService = roleService;
            _storeService = storeService;
        }

        public SelectList RolesSelectList { get; set; }

        public IList<Account> Account { get; set; } = default!;

        public IList<Store> Stores { get; set; } = default!;

        public SelectList StoresSelectList { get; set; }

        public List<SelectListItem> IsActiveOptions { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "true", Text = "Active" },
            new SelectListItem { Value = "false", Text = "Inactive"}
        };

        private void UpdatePageData(string searchKeyword = null)
        {
            var roles = _roleService.GetAll();
            RolesSelectList = new SelectList(roles, "Id", "Name");

            var stores = _storeService.GetAll();
            StoresSelectList = new SelectList(stores, "Id", "Name");

            if (string.IsNullOrEmpty(searchKeyword))
            {
                Account = _accountService.GetAllWithIncludeRoleAndStore();
            }
            else
            {
                Account = _accountService.GetByName(searchKeyword);
            }
        }

        public IActionResult OnGet()
        {
            var loginSession = HttpContext.Session.GetString("account");
            if (loginSession == null)
            {
                TempData["ErrorLogin"] = "You need to login to access this page";
                return RedirectToPage("../Login");
            }
            else if (!loginSession.Equals("admin"))
            {
                TempData["ErrorAuthorize"] = "You don't have permission to access this page";
                return RedirectToPage("../Error");
            }
            //Account = _accountService.GetAllWithIncludeRoleAndStore();
            //var roles = _roleService.GetAll(); 
            //RolesSelectList = new SelectList(roles, "Id", "Name");

            //var stores = _storeService.GetAll();

            //StoresSelectList = new SelectList(stores, "Id", "Name");
            UpdatePageData();

            return Page();
        }


        public IActionResult OnPostEditAccount(int id)
        {
            var isActive = HttpContext.Request.Form["IsActive"];
            var roleId = int.Parse(HttpContext.Request.Form["RoleId"]);
            var storeId = int.Parse(HttpContext.Request.Form["StoreId"]);
            var accountToUpdate = _accountService.GetById(id);
            accountToUpdate.IsActive = isActive == "true" ? true : false;
            accountToUpdate.RoleId = roleId;
            accountToUpdate.StoreId = roleId==1 ? storeId : null;
            _accountService.Update(accountToUpdate);
            HttpContext.Session.SetString("UpdateSuccessMessage", "Account updated successfully!");

            return RedirectToPage("./Index"); 
        }       
        public IActionResult OnPost()
        {
           

            var loginSession = HttpContext.Session.GetString("account");
            if (loginSession == null)
            {
                TempData["ErrorLogin"] = "You need to login to access this page";
                return RedirectToPage("../Login");
            }
            else if (!loginSession.Equals("admin"))
            {
                TempData["ErrorAuthorize"] = "You don't have permission to access this page";
                return RedirectToPage("../Error");
            }

            string searchKeyword = Request.Form["search"];
            UpdatePageData(searchKeyword);
            return Page();
        }
    }
}
