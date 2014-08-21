namespace GzipS3Client.Configuration
{
    public class CustomAmazonS3Configuration : CustomAmazonConfiguration, IAmazonS3Configuration
    {
        public string ServiceUrl { get; set; }
        public string BucketName { get; set; }
    }
}