using System.IO;

namespace GzipS3Client
{
    public interface IFileContent
    {
        string Key { get; }
        Stream ContentStream { get; }
    }
}