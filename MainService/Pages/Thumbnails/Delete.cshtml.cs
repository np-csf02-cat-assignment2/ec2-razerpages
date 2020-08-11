using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MainService.Data;
using MainService.Models;
using MainService.Services.Thumbnail;

namespace MainService.Pages_Thumbnails
{
    public class DeleteModel : PageModel
    {
        private readonly ThumbnailService _thumbnailService;

        public DeleteModel(ThumbnailService thumbnailService)
        {
            _thumbnailService = thumbnailService;
        }

        [BindProperty]
        public ThumbnailOverview ThumbnailOverview { get; set; }

        public IActionResult OnGet(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ThumbnailOverview = new ThumbnailOverview
            {
                ThumbnailUrl = _thumbnailService.GetPresignedURL(id),
                FileName = id
            };

            return Page();
        }

        public IActionResult OnPost(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _thumbnailService.DeleteThumbnail(id);

            return RedirectToPage("./Index");
        }
    }
}
