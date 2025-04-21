using System.Text.Json.Serialization;

namespace MicroNpmRegistry.Domain.Entities.Models
{
    public class NpmVersion
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("main")]
        public string Main { get; set; }

        [JsonPropertyName("scripts")]
        public Dictionary<string, string> Scripts { get; set; }

        [JsonPropertyName("dependencies")]
        public Dictionary<string, string> Dependencies { get; set; }

        [JsonPropertyName("dist")]
        public NpmDist Dist { get; set; }
    }

}
