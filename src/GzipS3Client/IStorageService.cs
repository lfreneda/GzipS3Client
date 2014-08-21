using System.Threading.Tasks;

namespace GzipS3Client
{
    public interface IStorageService
    {
        void Save(IFileContent fileContent);
        void SaveAsync(IFileContent fileContent);

        IFileContent Get(string key);
        Task<IFileContent> GetAsync(string key);
    }
}