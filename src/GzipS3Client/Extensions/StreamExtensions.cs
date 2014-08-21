using System.IO;
using System.Text;

namespace GzipS3Client.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ReadBytes(this Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static string ReadString(this Stream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}