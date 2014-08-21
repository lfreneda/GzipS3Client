using System.IO;
using System.Text;

namespace GzipS3Client
{
    public class StringFileContent : FileContent
    {
        public StringFileContent(string key, string contentString)
            : base(key, new MemoryStream(Encoding.UTF8.GetBytes(contentString)))
        {

        }
    }
}