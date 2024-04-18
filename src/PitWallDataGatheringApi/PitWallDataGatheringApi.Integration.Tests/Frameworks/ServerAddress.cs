namespace PitWallDataGatheringApi.Integration.Tests.Frameworks;

public readonly struct ServerAddress(string serverAddress)
{
    public Uri Uri { get; } = new(serverAddress);

    public readonly HttpClient CreateHttpClient()
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = Uri;
        return client;
    }
}