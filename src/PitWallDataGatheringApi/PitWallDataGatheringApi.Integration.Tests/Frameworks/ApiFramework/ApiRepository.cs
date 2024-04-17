using System.Text;
using Newtonsoft.Json;

namespace PitWallDataGatheringApi.Integration.Tests.Frameworks.ApiFramework;

public sealed class ApiRepository(ServerAddress serverAddress)
{
    public async Task<HttpResponseMessage> SendToApi(
        object model,
        RelativeUri requestUri)
    {
        var jsonData = SerializeAsJson(model);

        var content = ToStringContent(jsonData);

        var response = await serverAddress.CreateHttpClient().PostAsync(requestUri.Uri, content);

        return response;
    }

    private static string SerializeAsJson(object model)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.Formatting = Formatting.Indented;

        string jsonData = JsonConvert.SerializeObject(model, settings);

        return jsonData;
    }
    
    private static StringContent ToStringContent(string jsonData)
    {
        StringContent content = new StringContent(
            jsonData,
            Encoding.UTF8,
            "application/json");
        
        return content;
    }
}