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

namespace BookStoreRazorPage.Pages.StorePages
{
    public class EditModel : PageModel
    {
        private readonly IStoreService _storeService;

        public EditModel(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [BindProperty]
        public Store Store { get; set; } = default!;

        public IActionResult OnGet(int? id)
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

            if (id == null)
            {
                return NotFound();
            }

            ViewData["IsActive"] = new SelectList(new[]
                    {
                    new { Value = true, Text = "Active" },
                    new { Value = false, Text = "Inactive" }
                }, "Value", "Text");

            var store = _storeService.GetById((int)id);
            if (store == null)
            {
                return NotFound();
            }
            Store = store;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updateStore = _storeService.GetById(Store.Id);
                    _storeService.Update(updateStore!);

                    TempData["ResultSuccess"] = "Update Successfully";
                    return RedirectToPage("./Index");
                }

                TempData["ResultFailed"] = "Error Occurred. Cannot Update";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error Occurred. Please contact admin";
                Console.WriteLine(ex.ToString());
                return RedirectToPage("../Error");
            }
        }
    }
}
