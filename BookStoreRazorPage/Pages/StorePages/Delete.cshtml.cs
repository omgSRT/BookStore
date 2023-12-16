using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;

namespace BookStoreRazorPage.Pages.StorePages
{
    public class DeleteModel : PageModel
    {
        private readonly IStoreService _storeService;

        public DeleteModel(StoreService storeService)
        {
            _storeService = storeService;
        }

        [BindProperty]
      public Store Store { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            try
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

                var store = _storeService.GetById((int)id);

                if (store == null)
                {
                    return NotFound();
                }
                else
                {
                    Store = store;
                }
                return Page();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error Occurred. Please contact admin";
                Console.WriteLine(ex.ToString());
                return RedirectToPage("../Error");
            }
        }

        public IActionResult OnPost(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var store = _storeService.GetById((int)id);

                if (store != null)
                {
                   
                    _storeService.Delete(Store);

                    TempData["ResultSuccess"] = "Delete Successfully";
                    return RedirectToPage("./Index");
                }

                TempData["ResultFailed"] = "Error Occurred. Cannot Delete";
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
