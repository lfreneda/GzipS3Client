using System.IO;

namespace GzipS3Client
{
    public interface IFileContent
    {
        string Url { get; set; }
        string Key { get; }
        byte[] Content { get; }
    }
}