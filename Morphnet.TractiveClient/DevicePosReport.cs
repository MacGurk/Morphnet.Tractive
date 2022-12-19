namespace Morphnet.TractiveClient;

public class DevicePosReport
{
    public int time { get; set; }
    public int time_rcvd { get; set; }
    public string sensor_used { get; set; }
    public object pos_status { get; set; }
    public List<double> latlong { get; set; }
    public double speed { get; set; }
    public int pos_uncertainty { get; set; }
    public string _id { get; set; }
    public string _type { get; set; }
    public string _version { get; set; }
    public int altitude { get; set; }
    public string report_id { get; set; }
    public object nearby_user_id { get; set; }
    public object power_saving_zone_id { get; set; }
}