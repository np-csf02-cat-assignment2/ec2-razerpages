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
    public class IndexModel : PageModel
    {
        private readonly ThumbnailService _thumbnailService;

        public IndexModel(ThumbnailService thumbnailService)
        {
            _thumbnailService = thumbnailService;
        }

        public List<ThumbnailOverview> ThumbnailOverview { get; set; }

        public async Task OnGetAsync()
        {
            ThumbnailOverview = await _thumbnailService.ListThumbnailsAsync();
        }
    }
}
