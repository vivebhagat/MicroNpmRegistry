namespace MicroNpmRegistry.Entities.Models
{
    public class PackageMetadata
    {
        public string name { get; set; }
        public string version { get; set; }
        public Dictionary<string, VersionInfo> versions { get; set; }
        public string description { get; set; }
        public Distribution dist { get; set; }
    }
}
