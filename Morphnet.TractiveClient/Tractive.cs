using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Morphnet.TractiveClient;

public class Tractive
{
    private const string TractiveBaseUrl = "https://graph.tractive.com/4";
    private const string TractiveClientId = "5728aa1fc9077f7c32000186";
    private readonly HttpClient _httpClient;

    private string _token = string.Empty;
    private DateTimeOffset _tokenExpire;
    private string _userId = string.Empty;

    private readonly string _login;
    private readonly string _password;
    

    public Tractive(HttpClient httpClient, string login, string password)
    {
        _httpClient = httpClient;
        _login = login;
        _password = password;
    }

    public async Task Authenticate()
    {
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("platform_email", _login),
            new KeyValuePair<string, string>("platform_token", _password),
            new KeyValuePair<string, string>("grant_type", "tractive")
        });
        
        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{TractiveBaseUrl}/auth/token");
        requestMessage.Headers.Add("x-tractive-client", TractiveClientId);
        requestMessage.Headers.Add("accept", "application/json, text/plain, */*");
        requestMessage.Content = formContent;

        using var response = await _httpClient.SendAsync(requestMessage);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.StatusCode.ToString());
        }

        var authContent = await response.Content.ReadFromJsonAsync<AuthResponse>();
        _token = authContent!.access_token;
        _tokenExpire = DateTimeOffset.FromUnixTimeMilliseconds(1672068505);
        _userId = authContent!.user_id;
    }

    public async Task<List<Tracker>> GetTrackers()
    {
        if (!TokenIsValid())
        {
            await Authenticate();
        }

        using var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{TractiveBaseUrl}/user/{_userId}/trackers");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        requestMessage.Headers.Add("x-tractive-client", TractiveClientId);
        requestMessage.Headers.Add("accept", "application/json, text/plain, */*");

        using var response = await _httpClient.SendAsync(requestMessage);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.StatusCode.ToString());
        }
        
        var trackers = await response.Content.ReadFromJsonAsync<List<Tracker>>();
        return trackers ?? new List<Tracker>();
    }

    public async Task<(double lat, double lon)?> GetTrackerPosition(string trackerId)
    {
        if (!TokenIsValid())
        {
            await Authenticate();
        }
        
        using var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{TractiveBaseUrl}/device_pos_report/{trackerId}");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        requestMessage.Headers.Add("x-tractive-client", TractiveClientId);
        requestMessage.Headers.Add("accept", "application/json, text/plain, */*");
    
        using var response = await _httpClient.SendAsync(requestMessage);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.StatusCode.ToString());
        }

        var trackerPosition = await response.Content.ReadFromJsonAsync<DevicePosReport>();

        if (trackerPosition == null)
        {
            return null;
        }
        return (lat: trackerPosition.latlong[0], lon: trackerPosition.latlong[1]);
    }

    private bool TokenIsValid()
    {
        return !string.IsNullOrEmpty(_token) && _tokenExpire <= DateTimeOffset.Now;
    }
}