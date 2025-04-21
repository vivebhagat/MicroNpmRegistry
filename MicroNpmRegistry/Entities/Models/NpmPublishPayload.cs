using System.Text.Json.Serialization;

namespace MicroNpmRegistry.Entities.Models
{
    public class NpmPublishPayload
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("dist-tags")]
        public Dictionary<string, string> DistTags { get; set; }

        [JsonPropertyName("versions")]
        public Dictionary<string, NpmVersion> Versions { get; set; }

        [JsonPropertyName("_attachments")]
        public Dictionary<string, NpmAttachment> Attachments { get; set; }
    }

}
