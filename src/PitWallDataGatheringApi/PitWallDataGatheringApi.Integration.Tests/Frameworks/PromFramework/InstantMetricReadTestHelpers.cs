using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NFluent;

namespace PitWallDataGatheringApi.Integration.Tests.Frameworks.PromFramework;

internal static class InstantMetricReadTestHelpers
{
    public static async Task<string?> ReadInstantQueryResult(
        string metric, 
        string label, 
        string labelValue, 
        string prometheusServerAddress)
    {
        var queryPath = $"api/v1/query?query={metric}{{{label}=%22{labelValue}%22}}";

        return await ReadInstantQueryResult(queryPath,
            prometheusServerAddress, new Uri(prometheusServerAddress));
    }

    public static async Task<HttpResponseMessage> SendToApi(
        object model, 
        string prometheusServerAddress, 
        string requestUri)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(prometheusServerAddress);

        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.Formatting = Formatting.Indented;

        string jsonData = JsonConvert.SerializeObject(model, settings);

        StringContent content = new StringContent(
            jsonData,
            Encoding.UTF8,
            "application/json");

        var response = await client.PostAsync(requestUri, content);
        return response;
    }
    
    private static async Task<string?> ReadInstantQueryResult(
        string queryPath, 
        string timeSerieUri,
        Uri clientBaseAddress)
    {
        HttpClient client = new HttpClient();

        client.BaseAddress = clientBaseAddress;

        var result = await client.GetAsync(queryPath);

        Check.That(result.StatusCode).IsEqualTo(HttpStatusCode.OK);

        var stringified = await result.Content.ReadAsStringAsync();

        JObject json = (JObject)JsonConvert.DeserializeObject(stringified, new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
        });

        if (json == null)
        {
            throw new NoResultException(timeSerieUri, queryPath);
        }

        if (json["data"]["result"].Count() == 0)
        {
            throw new NoDataFoundException(timeSerieUri, queryPath);
        }

        string? intermediary = json["data"]?["result"]?[0]?["value"][1]?.ToString();
        return intermediary;
    }

}