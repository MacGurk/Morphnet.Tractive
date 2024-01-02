using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Morphnet.TractiveClient;

public class Tractive(string login, string password)
{
    private const string TractiveBaseUrl = "https://graph.tractive.com/4";
    private const string TractiveClientId = "5728aa1fc9077f7c32000186";
    
    private readonly HttpClient _httpClient = new();

    private string _token = string.Empty;
    private DateTimeOffset _tokenExpire;
    private string _userId = string.Empty;

    public async Task AuthenticateAsync()
    {
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("platform_email", login),
            new KeyValuePair<string, string>("platform_token", password),
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
        _token = authContent!.AccessToken;
        _tokenExpire = DateTimeOffset.FromUnixTimeMilliseconds(1672068505);
        _userId = authContent!.UserId;
    }

    public async Task<List<Tracker>> GetTrackersAsync()
    {
        if (!TokenIsValid())
        {
            await AuthenticateAsync();
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

    public async Task<DevicePosReport?> GetDevicePosReportAsync(string trackerId)
    {
        if (!TokenIsValid())
        {
            await AuthenticateAsync();
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

        return trackerPosition;
    }

    private bool TokenIsValid()
    {
        return !string.IsNullOrEmpty(_token) && _tokenExpire <= DateTimeOffset.Now;
    }
}