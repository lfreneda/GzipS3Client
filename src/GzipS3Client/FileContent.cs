using System.IO;

namespace GzipS3Client
{
    public class FileContent : IFileContent
    {
        public FileContent(string key, Stream contentStream)
        {
            Key = key;
            ContentStream = contentStream;
        }

        public string Url { get; set; }
        public string Key { get; set; }
        public Stream ContentStream { get; set; }
    }
}