namespace MicroNpmRegistry.Infrastructure.Storage
{
    public interface IFileService
    {
        void WriteAllBytes(string path, byte[] bytes);
        void WriteAllText(string path, string content);
        Boolean Exists(string path);
        string ReadAllText(string path);
        string GetPath(string decodedFileName);
    }
}
