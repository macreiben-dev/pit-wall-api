using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NFluent;
using PitWallDataGatheringApi.Models.Apis;
using System.Globalization;
using System.Net;
using System.Text;

namespace PitWallDataGatheringApi.Integration.Tests
{
    public class InstantMetricRead
    {
        [Fact]
        public async Task GIVEN_airTemp_AND_using_instantQueries_THEN_retrieve_value()
        {
            TelemetryModel model = CreateApiModel();

            model.PilotName = "IntegrationTest_airTemp";

            model.AirTemperature = 10.0;

            {
                HttpResponseMessage response = await SendToApi(model);

                Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
            }
            {
                //var queryPath = "api/v1/query?query=pitwall_air_temperature_celsius{Pilot=%22IntegrationTest_airTemp%22}";

                string intermediary = await ReadInstantQueryResult(
                    "pitwall_air_temperature_celsius", 
                    "Pilot", 
                    "IntegrationTest_airTemp");

                var actual = Double.Parse(intermediary, CultureInfo.InvariantCulture);

                Check.That(actual).IsEqualTo(10.0);
            }
        }


        [Fact]
        public async void GIVEN_laptimeSeconds_WHEN_using_instantQueries_THEN_retrieve_value()
        {
            TelemetryModel model = new TelemetryModel();
            model.SimerKey = "some_test_looking_value23";

            model.PilotName = "IntegrationTest_laptime";

            model.LaptimeSeconds = 122;

            {
                HttpResponseMessage response = await SendToApi(model);

                Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
            }

            {
                var queryPath = "api/v1/query?query=pitwall_laptimes_seconds{Pilot=%22IntegrationTest_laptime%22}";

                string intermediary = await ReadInstantQueryResult(
                   "pitwall_laptimes_seconds",
                   "Pilot",
                   "IntegrationTest_laptime");

                var actual = Double.Parse(intermediary, CultureInfo.InvariantCulture);

                Check.That(actual).IsEqualTo(122.0);
            }
        }

        private static TelemetryModel CreateApiModel()
        {
            TelemetryModel model = new TelemetryModel();
            model.SimerKey = "some_test_looking_value23";
            return model;
        }

        private static async Task<string> ReadInstantQueryResult(string metric, string label, string labelValue)
        {
            var queryPath = $"api/v1/query?query={metric}{{{label}=%22{labelValue}%22}}";

            return await ReadInstantQueryResult(queryPath);
        }

        private static async Task<string> ReadInstantQueryResult(string queryPath)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:10100");

            var result = await client.GetAsync(queryPath);

            Check.That(result.StatusCode).IsEqualTo(HttpStatusCode.OK);

            var stringified = await result.Content.ReadAsStringAsync();

            JObject json = (JObject)JsonConvert.DeserializeObject(stringified, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
            });

            string intermediary = json["data"]["result"][0]["value"][1].ToString();
            return intermediary;
        }

        private static async Task<HttpResponseMessage> SendToApi(TelemetryModel model)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:32773");

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;

            string jsonData = JsonConvert.SerializeObject(model, settings);

            StringContent content = new StringContent(
                jsonData,
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/api/Telemetry", content);
            return response;
        }
    }
}