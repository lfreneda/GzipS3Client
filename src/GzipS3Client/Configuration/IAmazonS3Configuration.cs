namespace GzipS3Client.Configuration
{
    public interface IAmazonS3Configuration : IAmazonConfiguration
    {
        string ServiceUrl { get; }
        string BucketName { get; }
    }
}