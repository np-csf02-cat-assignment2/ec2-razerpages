using System.ComponentModel.DataAnnotations;

namespace MainService.Services.Thumbnail
{
    public class ThumbnailOverview
    {
        [DataType(DataType.Url)]
        public string ThumbnailUrl { get; set; }
        public string FileName { get; set; }
    }
}
