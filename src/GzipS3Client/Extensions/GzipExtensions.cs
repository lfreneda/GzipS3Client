using System.IO;
using System.IO.Compression;
using System.Text;

namespace GzipS3Client.Extensions
{
    public static class GzipExtensions
    {
        public static byte[] GzipCompress(this Stream stream)
        {
            byte[] bytesCompressed = null;

            using (var compressStream = new MemoryStream())
            {
                using (var compressor = new GZipStream(compressStream, CompressionMode.Compress))
                {
                    stream.Position = 0;

                    stream.CopyTo(compressor);
                    compressor.Close();
                    bytesCompressed = compressStream.ToArray();
                }
            }

            return bytesCompressed;
        }

        public static Stream GzipDescompress(this byte[] bytes)
        {
            var decompressedStream = new MemoryStream();

            using (var compressStream = new MemoryStream(bytes))
            using (var decompressor = new GZipStream(compressStream, CompressionMode.Decompress))
            {
                decompressor.CopyTo(decompressedStream);
            }

            decompressedStream.Position = 0;

            return decompressedStream;
        }
    }
}