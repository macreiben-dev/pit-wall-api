using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NFluent;
using PitWallDataGatheringApi.Integration.Tests;
using PitWallDataGatheringApi.Models.Apis.v1;
using System.Net;
using System.Text;

internal static class InstantMetricReadTestHelpers
{

    private static TelemetryModel CreateApiModel()
    {
        TelemetryModel model = new TelemetryModel();
        model.SimerKey = "some_test_looking_value23";
        return model;
    }

    public static async Task<string> ReadInstantQueryResult(
        string metric, 
        string label, 
        string labelValue, 
        string timeSerieUri)
    {
        var queryPath = $"api/v1/query?query={metric}{{{label}=%22{labelValue}%22}}";

        return await ReadInstantQueryResult(queryPath,
            timeSerieUri);
    }

    private static async Task<string> ReadInstantQueryResult(string queryPath, string timeSerieUri)
    {
        HttpClient client = new HttpClient();

        client.BaseAddress = new Uri(timeSerieUri);

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

        string intermediary = json["data"]["result"][0]["value"][1].ToString();
        return intermediary;
    }

    public static async Task<HttpResponseMessage> SendToApi(object model, string targetApi)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(targetApi);

        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.Formatting = Formatting.Indented;

        string jsonData = JsonConvert.SerializeObject(model, settings);

        StringContent content = new StringContent(
            jsonData,
            Encoding.UTF8,
            "application/json");

        var response = await client.PostAsync("/api/v1/Leaderboard", content);
        return response;
    }
}