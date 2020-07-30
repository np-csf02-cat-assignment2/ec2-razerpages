using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MainService.Data;
using MainService.Models;

namespace MainService.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly MainService.Data.MainServiceContext _context;

        public IndexModel(MainService.Data.MainServiceContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get;set; }

        public async Task OnGetAsync()
        {
            Customer = await _context.Customer.ToListAsync();
        }
    }
}
