namespace GzipS3Client.Configuration
{
    public class CustomAmazonConfiguration : IAmazonConfiguration
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
}