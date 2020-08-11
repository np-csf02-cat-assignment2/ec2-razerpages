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
    public class DetailsModel : PageModel
    {
        private readonly MainService.Data.MainServiceDbContext _context;

        public DetailsModel(MainService.Data.MainServiceDbContext context)
        {
            _context = context;
        }

        public ContactUs ContactUs { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContactUs = await _context.ContactUs.FirstOrDefaultAsync(m => m.ID == id);

            if (ContactUs == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
