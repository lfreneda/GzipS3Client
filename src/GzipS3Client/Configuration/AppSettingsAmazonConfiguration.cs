using System.Configuration;

namespace GzipS3Client.Configuration
{
    public class AppSettingsAmazonConfiguration : IAmazonConfiguration
    {
        public string AccessKey { get { return ConfigurationManager.AppSettings["aws.accessKey"]; } }
        public string SecretKey { get { return ConfigurationManager.AppSettings["aws.secretKey"]; } }
    }
}