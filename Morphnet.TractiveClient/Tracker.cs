using System.Text.Json.Serialization;

namespace Morphnet.TractiveClient;

public class Tracker
{
    [JsonPropertyName("_id")]
    public string Id { get; set; } = null!;
    
    [JsonPropertyName("_type")]
    public string Type { get; set; } = null!;

    [JsonPropertyName("_version")]
    public string Version { get; set; } = null!;
}