using System.Text.Json.Serialization;

namespace MicroNpmRegistry.Domain.Entities.Models
{
    public class NpmDist
    {
        [JsonPropertyName("shasum")]
        public string Shasum { get; set; }

        [JsonPropertyName("tarball")]
        public string Tarball { get; set; }
    }

}
