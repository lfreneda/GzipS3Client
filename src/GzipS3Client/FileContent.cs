using System.IO;

namespace GzipS3Client
{
    public class FileContent : IFileContent
    {
        public FileContent(string key, byte[] content)
        {
            Key = key;
            Content = content;
        }

        public string Url { get; set; }
        public string Key { get; set; }
        public byte[] Content { get; set; }
    }
}