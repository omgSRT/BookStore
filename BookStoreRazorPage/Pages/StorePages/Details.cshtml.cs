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
    public class DetailsModel : PageModel
    {
        private readonly IStoreService _storeService;

        public DetailsModel()
        {
            _storeService = new StoreService();
        }

      public Store Store { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var store = _storeService.GetAll()
                    .Where(x => x.Id == (int)id).FirstOrDefault();
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
    }
}
