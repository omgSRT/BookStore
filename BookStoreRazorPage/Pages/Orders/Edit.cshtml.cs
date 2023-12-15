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
using Microsoft.VisualStudio.Debugger.Contracts;

namespace BookStoreRazorPage.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly IOrderService _service;

        public EditModel()
        {
            _service = new OrderService();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            string role = HttpContext.Session.GetString("account");
            int accountId = (int)HttpContext.Session.GetInt32("accountId");
            if (role != null)
            {
                if (role.Equals("customer") || role.Equals("seller"))
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var order = _service.GetOrderById(id);
                    if (order == null)
                    {
                        return NotFound();
                    }
                    Order = order;
                    IList<string> listStatus = new List<string> { "Processing", "Completed", "Canceled" };
                    ViewData["Status"] = new SelectList(listStatus);
                    return Page();
                }
            }
            return RedirectToPage("../Logout");
            
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPostAsync()
        {
            

            try
            {
                if(ModelState.IsValid)
                {
                    var _updateOrder = _service.GetOrderById(Order.Id);
                    if( _updateOrder != null)
                    {
                        _updateOrder = Order;
                        _service.UpdateOrder(_updateOrder);
                        var role = HttpContext.Session.GetString("account");
                        if (role.Equals("seller"))
                        {
                            return RedirectToPage("../Orders/IndexSeller");
                        }else if (role.Equals("customer"))
                        {
                            return RedirectToPage("../Orders/IndexCustomer");
                        }
                        return RedirectToPage("Logout");
                    }
                    
                }return Page();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                TempData["Error"] = "Error Occurred. Please contact admin";
                Console.WriteLine(ex.ToString());
                return RedirectToPage("../Error");
            }

        }

    }
}
