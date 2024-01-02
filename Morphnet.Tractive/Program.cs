using Morphnet.TractiveClient;

var username = Environment.GetEnvironmentVariable("TRACTIVE_USERNAME");
var password = Environment.GetEnvironmentVariable("TRACTIVE_PASSWORD");
if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
{
    Console.WriteLine("Please set TRACTIVE_USERNAME and TRACTIVE_PASSWORD environment variables.");
    return;
}

var tractive = new Tractive(username, password);

await tractive.AuthenticateAsync();

var trackers = await tractive.GetTrackersAsync();

foreach (var tracker in trackers)
{
    Console.WriteLine(tracker.Id);
    var deviceReport = await tractive.GetDevicePosReportAsync(tracker.Id);
    Console.WriteLine($"lat: {deviceReport.LatLong[0]}, long: {deviceReport.LatLong[1]}, time: {DateTimeOffset.FromUnixTimeSeconds(deviceReport.Time).ToLocalTime()}");
}