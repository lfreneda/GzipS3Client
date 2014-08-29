using System.IO;
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
            var client = CreateS3Client();

            using (var stream = new MemoryStream(fileContent.Content))
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

                    client.PutObject(putRequest);
                }
            }
        }

        public string CreateUrl(IFileContent fileContent)
        {
            return CreateUrl(fileContent.Key);
        }

        public string CreateUrl(string key)
        {
            var serviceUrlWithBucketName = _s3Configuration.ServiceUrl.Replace("http://s3-", string.Format("http://{0}.s3-", _s3Configuration.BucketName));

            if (serviceUrlWithBucketName.EndsWith("/"))
            {
                return serviceUrlWithBucketName + key;
            }

            return serviceUrlWithBucketName + "/" + key;
        }

        public IFileContent Get(string key)
        {
            var client = CreateS3Client();

            var getRequest = new GetObjectRequest
            {
                BucketName = _s3Configuration.BucketName,
                Key = key,
            };

            var response = client.GetObject(getRequest);

            var responseBytes = response.ResponseStream.ReadBytes();

            var content = responseBytes.GzipDescompress().ReadBytes();

            return new FileContent(key, content);
        }

        public bool ContainsFile(string key)
        {
            var client = CreateS3Client();

            try
            {
                client.GetObjectMetadata(new GetObjectMetadataRequest
                {
                    BucketName = _s3Configuration.BucketName,
                    Key = key,
                });

                return true;
            }
            catch (AmazonS3Exception ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound) return false
                return false;
            }
        }
    }
}