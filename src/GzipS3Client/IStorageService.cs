namespace GzipS3Client
{
    public interface IStorageService
    {
        void Save(IFileContent fileContent);
        IFileContent Get(string key);
        bool ContainsFile(string key);
        string CreateUrl(IFileContent fileContent);
    }
}