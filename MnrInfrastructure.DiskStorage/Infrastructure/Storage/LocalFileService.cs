using MicroNpmRegistry.Domain.Entities;
using Microsoft.Extensions.Options;

namespace MicroNpmRegistry.Infrastructure.Storage
{
    public class LocalFileService : IFileService
    {        
        private RegistrySettings RegsitrySettings { get; set; }
        public LocalFileService(IOptions<RegistrySettings> options)
        {
            this.RegsitrySettings = options.Value;   
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public async Task WriteAllByteAsync(string path, byte[] bytes)
        {
           await File.WriteAllBytesAsync(path, bytes); 
        }

        public async Task WriteAllTextAsync(string path, string content)
        {
            await File.WriteAllTextAsync(path, content);
        }

        public string GetPath(string filename)
        {
            return Path.Combine();
        }

        public void DeleteFile(string tarBallFilePath)
        {
            File.Delete(tarBallFilePath);
        }
    }
}
