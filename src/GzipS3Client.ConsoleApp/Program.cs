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
                ServiceUrl = "http://s3-sa-east-1.amazonaws.com",
                BucketName = ""
            };

            IStorageService storage = new AmazonStorageService(s3Config);

            var fileStream = new FileStream(@"sample_file_to_upload.txt", FileMode.Open);

            var bytes = fileStream.ReadBytes();

            storage.Save(new FileContent("file_key1", bytes));

            var contentBack = storage.Get("file_key1");

            var contentAsString = System.Text.Encoding.UTF8.GetString(contentBack.Content);

            Console.WriteLine(contentAsString);

            Console.Read();
        }
    }
}
