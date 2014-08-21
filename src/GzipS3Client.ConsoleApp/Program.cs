using System;
using System.IO;
using GzipS3Client.Configuration;
 using GzipS3Client.Extensions;

namespace GzipS3Client.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var s3Config = new CustomAmazonS3Configuration
            {
                AccessKey = "",
                SecretKey = "",
                ServiceUrl = "",
                BucketName = ""
            };

            IStorageService storage = new AmazonStorageService(s3Config);

            var fileStream = new FileStream(@"sample_file_to_upload.txt", FileMode.Open);

            storage.Save(new FileContent("file_key1", fileStream));

            var fileContent = storage.Get("file_key1");

            var contentAsString = fileContent.ContentStream.ReadString();

            Console.WriteLine(contentAsString);

            Console.Read();
        }
    }
}
