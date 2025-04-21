using MicroNpmRegistry.Domain.Entities;
using Microsoft.Extensions.Options;

namespace MicroNpmRegistry.Infrastructure.Storage
{
    public class LocalFileService : IFileService
    {        
        private RegistrySettings regsitrySettings { get; set; }
        public LocalFileService(IOptions<RegistrySettings> options)
        {
            this.regsitrySettings = options.Value;   
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public void WriteAllBytes(string path, byte[] bytes)
        {
            File.WriteAllBytes(path, bytes);  
        }

        public void WriteAllText(string path, string content)
        {
             File.WriteAllText(path, content);
        }

        public string GetPath(string filename)
        {
            return Path.Combine();
        }
    }
}
