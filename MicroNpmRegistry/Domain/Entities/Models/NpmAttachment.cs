using System.Text.Json.Serialization;

namespace MicroNpmRegistry.Domain.Entities.Models
{

    public class NpmAttachment
    {
        [JsonPropertyName("content_type")]
        public string ContentType { get; set; }

        [JsonPropertyName("data")]
        public string Base64Data { get; set; }

        [JsonPropertyName("length")]
        public int Length { get; set; }
    }

}
