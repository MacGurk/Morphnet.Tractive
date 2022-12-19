namespace Morphnet.TractiveClient;

public class AuthResponse
{
    public string user_id { get; set; }
    public string client_id { get; set; }
    public int expires_at { get; set; }
    public string access_token { get; set; }
}