using System.Net;
using System.Security.Policy;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NFluent;
using PitWallDataGatheringApi.Integration.Tests.Frameworks;

namespace PitWallDataGatheringApi.Integration.Tests.PromFramework;

internal class PrometheusMetricRepository(ServerAddress serverAddress)
{
    public async Task<string?> ReadInstantQueryResult(
        string metric, 
        string label, 
        string labelValue)
    {
        var encodedValue = WebUtility.UrlEncode(labelValue);
        
        var queryPath = $"api/v1/query?query={metric}{{{label}=%22{encodedValue}%22}}";

        return await ReadInstantQueryResult(queryPath);
    }
    
    private async Task<string?> ReadInstantQueryResult(
        string queryPath)
    {
        var result = await serverAddress.CreateHttpClient().GetAsync(queryPath);

        Check.That(result.StatusCode).IsEqualTo(HttpStatusCode.OK);

        var json = await ToJson(result);

        if (json == null)
        {
            throw new NoResultException(serverAddress.Uri, queryPath);
        }

        if (json["data"]?["result"]?.Count() == 0)
        {
            throw new NoDataFoundException(serverAddress.Uri, queryPath);
        }

        string? intermediary = json["data"]?["result"]?[0]?["value"]?[1]?.ToString();
        
        return intermediary;
    }

    private static async Task<JObject?> ToJson(HttpResponseMessage result)
    {
        var stringified = await result.Content.ReadAsStringAsync();

        JObject? json = JsonConvert.DeserializeObject(stringified, new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
        }) as JObject;
        
        return json;
    }
}