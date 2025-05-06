namespace MicroNpmRegistry.Infrastructure.Storage
{
    public interface IFileService
    {
        Task WriteAllByteAsync(string path, byte[] bytes);
        Task WriteAllTextAsync(string path, string content);
        Boolean Exists(string path);
        string ReadAllText(string path);
        string GetFullPathForFile(string decodedFileName);
        void DeleteFile(string tarBallFilePath);
    }
}
