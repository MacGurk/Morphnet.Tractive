using System.Text.Json.Serialization;

namespace Morphnet.TractiveClient;

public class DevicePosReport
{
    [JsonPropertyName("time")]
    public int Time { get; set; }
    
    [JsonPropertyName("time_rcvd")]
    public int TimeRcvd { get; set; }
    
    [JsonPropertyName("sensor_used")]
    public string SensorUsed { get; set; } = null!;

    [JsonPropertyName("pos_status")]
    public object PosStatus { get; set; } = null!;

    [JsonPropertyName("latlong")]
    public List<double> LatLong { get; set; } = null!;

    [JsonPropertyName("speed")]
    public double? Speed { get; set; }
    
    [JsonPropertyName("pos_uncertainty")]
    public int PosUncertainty { get; set; }
    
    [JsonPropertyName("_id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("_type")]
    public string Type { get; set; } = null!;

    [JsonPropertyName("_version")]
    public string Version { get; set; } = null!;

    [JsonPropertyName("altitude")]
    public int Altitude { get; set; }
    
    [JsonPropertyName("report_id")]
    public string ReportId { get; set; } = null!;

    [JsonPropertyName("nearby_user_id")]
    public object NearbyUserId { get; set; } = null!;

    [JsonPropertyName("power_saving_zone_id")]
    public object PowerSavingZoneId { get; set; } = null!;
}