using System.Text.Json.Serialization;

namespace Morphnet.TractiveClient;

public class AuthResponse
{
    [JsonPropertyName("user_id")]
    public string UserId { get; set; } = null!;

    [JsonPropertyName("client_id")]
    public string ClientId { get; set; } = null!;

    [JsonPropertyName("expires_at")]
    public int ExpiresAt { get; set; }
    
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = null!;
}