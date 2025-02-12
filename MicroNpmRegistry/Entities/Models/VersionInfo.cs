namespace MicroNpmRegistry.Entities.Models
{
    public class VersionInfo
    {
        public string name { get; set; }
        public string version { get; set; }
        public Distribution dist { get; set; }
    }
}
