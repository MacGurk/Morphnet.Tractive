using System.Text.Json.Serialization;

namespace Morphnet.TractiveClient;

public class TrackableObject
{
    [JsonPropertyName("_id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("hw_id")]
    public string HwId { get; set; } = null!;

    [JsonPropertyName("hw_type")]
    public string HwType { get; set; } = null!;

    [JsonPropertyName("_type")]
    public string Type { get; set; } = null!;

    [JsonPropertyName("geofence_sensitivity")]
    public string GeofenceSensitivity { get; set; } = null!;

    [JsonPropertyName("battery_save_mode")]
    public bool BatterySaveMode { get; set; }
    
    [JsonPropertyName("state")]
    public string State { get; set; } = null!;

    [JsonPropertyName("charging_state")]
    public string ChargingState { get; set; } = null!;

    [JsonPropertyName("battery_state")]
    public string BatteryState { get; set; } = null!;
}