using System.Configuration;

namespace GzipS3Client.Configuration
{
    public class AppSettingsAmazonS3Configuration : AppSettingsAmazonConfiguration
    {
        public string ServiceUrl { get { return ConfigurationManager.AppSettings["aws.serviceUrl"]; } }
        public string BucketName { get { return ConfigurationManager.AppSettings["aws.bucketName"]; } }
    }
}