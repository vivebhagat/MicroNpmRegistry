using System.Text.Json.Serialization;

namespace MicroNpmRegistry.Entities.Models
{
    public class NpmDist
    {
        [JsonPropertyName("shasum")]
        public string Shasum { get; set; }

        [JsonPropertyName("tarball")]
        public string Tarball { get; set; }
    }

}
