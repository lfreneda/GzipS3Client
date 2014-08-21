namespace GzipS3Client.Configuration
{
    public interface IAmazonConfiguration
    {
        string AccessKey { get; }
        string SecretKey { get; }
    }
}