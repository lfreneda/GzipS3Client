using System.Threading.Tasks;

namespace GzipS3Client
{
    public interface IStorageService
    {
        void SaveAsync(IFileContent fileContent);
        Task<IFileContent> GetAsync(string key);
        Task<bool> ContainsFile(string key);
    }
}