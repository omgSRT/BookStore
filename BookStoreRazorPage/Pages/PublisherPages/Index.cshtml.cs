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
    public class IndexModel : PageModel
    {
        private readonly IPublisherService _publisherService;

        public IndexModel(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        public IList<Publisher> Publisher { get;set; } = default!;
        [BindProperty]
        public string SearchValue { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public int curentPage { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public int count { get; set; }
        public int totalPages => (int)Math.Ceiling(Decimal.Divide(count, pageSize));

        public IActionResult OnGet()
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

                count = _publisherService.GetAll().Count();
                Publisher = _publisherService.GetAll()
                    .Skip((curentPage - 1) * pageSize).Take(pageSize)
                    .ToList();
                return Page();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error Occurred. Please contact admin";
                Console.WriteLine(ex.ToString());
                return RedirectToPage("../Error");
            }
        }
        public IActionResult OnPost()
        {
            if(SearchValue is null)
            {
                count = _publisherService.GetAll().Count();
                Publisher = _publisherService.GetAll()
                    .Skip((curentPage - 1) * pageSize).Take(pageSize)
                    .ToList();
                return Page();
            }
            else if(SearchValue.Trim().Length == 0)
            {
                count = _publisherService.GetAll().Count();
                Publisher = _publisherService.GetAll()
                    .Skip((curentPage - 1) * pageSize).Take(pageSize)
                    .ToList();
                return Page();
            }
            else
            {
                count = _publisherService.GetAll()
                    .Where(x => x.Name.Contains(SearchValue, StringComparison.OrdinalIgnoreCase))
                    .Count();
                Publisher = _publisherService.GetAll()
                    .Where(x => x.Name.Contains(SearchValue, StringComparison.OrdinalIgnoreCase))
                    .Skip((curentPage - 1) * pageSize).Take(pageSize)
                    .ToList();
                if(Publisher.Count == 0)
                {
                    TempData["ResultFailed"] = "There's no result matched " + SearchValue;
                    count = _publisherService.GetAll().Count();
                    Publisher = _publisherService.GetAll()
                        .Skip((curentPage - 1) * pageSize).Take(pageSize)
                        .ToList();
                }
                return Page();
            }
        }
    }
}
