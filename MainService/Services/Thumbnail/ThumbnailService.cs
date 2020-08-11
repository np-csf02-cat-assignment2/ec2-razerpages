using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace MainService.Services.Thumbnail
{
    public class ThumbnailService
    {
        private readonly string _uploadBucketName;
        private readonly string _downloadBucketName;
        private readonly RegionEndpoint _bucketRegion;
        private readonly AmazonS3Client _s3Client;
        private readonly string _accessKey;
        private readonly string _secretKey;
        public readonly string _sessionToken;

        public ThumbnailService(IOptions<ThumbnailServiceOptions> options)
        {
            _uploadBucketName = options.Value.UploadBucketName;
            _downloadBucketName = options.Value.DownloadBucketName;
            _bucketRegion = options.Value.RegionEndpoint ?? RegionEndpoint.USEast1;
            _accessKey = options.Value.AccessKey;
            _secretKey = options.Value.SecretKey;
            _sessionToken = options.Value.SessionToken;

            _s3Client = new AmazonS3Client(_accessKey, _secretKey, _sessionToken, _bucketRegion);
        }

        public async Task UploadThumbnailAsync(Stream fileStream, string fileName)
        {
            var fileTransferUtility = new TransferUtility(_s3Client);

            await fileTransferUtility.UploadAsync(fileStream, _uploadBucketName, fileName);
        }

        public string GetLargePresignedURL(string filePath)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _uploadBucketName,
                Key = filePath,
                Expires = DateTime.Now.AddMinutes(5)
            };

            return _s3Client.GetPreSignedURL(request);
        }

        public string GetPresignedURL(string filePath)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _downloadBucketName,
                Key = filePath,
                Expires = DateTime.Now.AddMinutes(5)
            };

            return _s3Client.GetPreSignedURL(request);
        }

        public async Task<List<ThumbnailOverview>> ListThumbnailsAsync()
        {
            var response = await _s3Client.ListObjectsV2Async(new ListObjectsV2Request
            {
                BucketName = _downloadBucketName,
                MaxKeys = 50
            });

            var thumbnails = new List<ThumbnailOverview>();

            foreach (var entry in response.S3Objects)
            {
                var thumbnailUrl = GetPresignedURL(entry.Key);

                thumbnails.Add(new ThumbnailOverview
                {
                    FileName = entry.Key,
                    ThumbnailUrl = thumbnailUrl
                });
            }

            return thumbnails;
        }

        public void DeleteThumbnail(string filePath)
        {
            Task.WaitAll(new Task[]
            {
                _s3Client.DeleteObjectAsync(_uploadBucketName, filePath),
                _s3Client.DeleteObjectAsync(_downloadBucketName, filePath)
            });
        }
    }
}
