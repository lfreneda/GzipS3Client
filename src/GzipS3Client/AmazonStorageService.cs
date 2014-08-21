using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using GzipS3Client.Configuration;
using GzipS3Client.Extensions;

namespace GzipS3Client
{
    public class AmazonStorageService : IStorageService
    {
        private readonly IAmazonS3Configuration _s3Configuration;

        public AmazonStorageService(IAmazonS3Configuration s3Configuration)
        {
            _s3Configuration = s3Configuration;
        }

        private IAmazonS3 CreateS3Client()
        {
            var accessKey = _s3Configuration.AccessKey;
            var secretKey = _s3Configuration.SecretKey;

            var client = AWSClientFactory.CreateAmazonS3Client(accessKey, secretKey, new AmazonS3Config
            {
                ServiceURL = _s3Configuration.ServiceUrl
            });

            return client;
        }

        public void Save(IFileContent fileContent)
        {
            SaveAsync(fileContent);
        }

        public async void SaveAsync(IFileContent fileContent)
        {
            var client = CreateS3Client();

            using (var stream = fileContent.ContentStream)
            {
                var bytesCompressed = stream.GzipCompress();

                using (var compressedStream = new MemoryStream(bytesCompressed))
                {
                    var putRequest = new PutObjectRequest
                    {
                        CannedACL = S3CannedACL.Private,
                        BucketName = _s3Configuration.BucketName,
                        InputStream = compressedStream,
                        Key = fileContent.Key,
                    };

                    var putResponse = await client.PutObjectAsync(putRequest);
                }
            }
        }

        public IFileContent Get(string key)
        {
            return GetAsync(key).Result;
        }

        public async Task<IFileContent> GetAsync(string key)
        {
            var client = CreateS3Client();

            var getRequest = new GetObjectRequest
            {
                BucketName = _s3Configuration.BucketName,
                Key = key,
            };

            var response = await client.GetObjectAsync(getRequest);

            var responseBytes = response.ResponseStream.ReadBytes();

            var memoryStream = responseBytes.GzipDescompress();

            return new FileContent(key, memoryStream);
        }
    }
}