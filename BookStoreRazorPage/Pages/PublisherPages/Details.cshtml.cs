﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;

namespace BookStoreRazorPage.Pages.PublisherPages
{
    public class DetailsModel : PageModel
    {
        private readonly IPublisherService _publisherService;

        public DetailsModel()
        {
            _publisherService = new PublisherService();
        }

      public Publisher Publisher { get; set; } = default!; 

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

                var publisher = _publisherService.GetById((int)id);
                if (publisher == null)
                {
                    return NotFound();
                }
                else
                {
                    Publisher = publisher;
                }
                return Page();
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Error Occurred. Please contact admin";
                Console.WriteLine(ex.ToString());
                return RedirectToPage("../Error");
            }
        }
    }
}
