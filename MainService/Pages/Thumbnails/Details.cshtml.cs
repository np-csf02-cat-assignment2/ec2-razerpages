using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MainService.Models;
using MainService.Services.Thumbnail;

namespace MainService.Pages_Thumbnails
{
    public class DetailsModel : PageModel
    {
        private readonly ThumbnailService _thumbnailService;

        public DetailsModel(ThumbnailService thumbnailService)
        {
            _thumbnailService = thumbnailService;
        }

        [BindProperty]
        public string fileName { get; set; }
        public string fileLargeURL { get; set; }
        public string fileURL { get; set; }

        public IActionResult OnGet(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            fileName = id;
            fileLargeURL = _thumbnailService.GetLargePresignedURL(id);
            fileURL = _thumbnailService.GetPresignedURL(id);

            return Page();
        }
    }
}
