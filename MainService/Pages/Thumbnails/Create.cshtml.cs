using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MainServices.Models;
using MainService.Services.Thumbnail;
using System.IO;
using Microsoft.AspNetCore.Http;
using System;

namespace MainService.Pages_Thumbnails
{
    public class CreateModel : PageModel
    {
        private readonly ThumbnailService _thumbnailService;
        public CreateModel(ThumbnailService thumbnailService)
        {
            _thumbnailService = thumbnailService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public IFormFile Upload { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var uploadFilePath = Guid.NewGuid().ToString() + Path.GetExtension(Upload.FileName);

            using (var memoryStream = new MemoryStream())
            {
                await Upload.CopyToAsync(memoryStream);

                await _thumbnailService.UploadThumbnailAsync(memoryStream, uploadFilePath);
            }

            return new ContentResult
            {
                Content = $"File successfuly uploaded: {uploadFilePath}"
            };
        }
    }
}
