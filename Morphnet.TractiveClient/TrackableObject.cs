namespace Morphnet.TractiveClient;

public class TrackableObject
{
    public string _id { get; set; }
    public string hw_id { get; set; }
    public string hw_type { get; set; }
    public string _type { get; set; }
    public string geofence_sensitivity { get; set; }
    public bool battery_save_mode { get; set; }
    public string state { get; set; }
    public string charging_state { get; set; }
    public string battery_state { get; set; }
}