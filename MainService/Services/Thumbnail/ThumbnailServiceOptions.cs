using Amazon;

namespace MainService.Services.Thumbnail
{
    public class ThumbnailServiceOptions
    {
        public string UploadBucketName { get; set; }
        public string DownloadBucketName { get; set; }
        public RegionEndpoint RegionEndpoint { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string SessionToken { get; set; }
    }
}
