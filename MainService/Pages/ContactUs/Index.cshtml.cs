using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MainService.Data;
using MainService.Models;

namespace MainService.Pages_ContactUs
{
    public class IndexModel : PageModel
    {
        private readonly MainService.Data.MainServiceDbContext _context;

        public IndexModel(MainService.Data.MainServiceDbContext context)
        {
            _context = context;
        }

        public IList<ContactUs> ContactUs { get;set; }

        public async Task OnGetAsync()
        {
            ContactUs = await _context.ContactUs.ToListAsync();
        }
    }
}
