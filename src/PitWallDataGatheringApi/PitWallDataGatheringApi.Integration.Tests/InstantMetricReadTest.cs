using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NFluent;
using PitWallDataGatheringApi.Models.Apis;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text;

namespace PitWallDataGatheringApi.Integration.Tests
{
    public class InstantMetricReadTest
    {
        private const string TimeSerieUri = "http://localhost:10100";
        private const string TargetApi = "http://localhost:32773";
        private const string PilotLabel = "Pilot";

        public class TestDataInstantMetrics : IEnumerable<object[]>
        {
            private const string SimerKey = "some_test_looking_value23";

            private readonly List<object[]> _inner = null;

            public TestDataInstantMetrics()
            {
                var data = new List<object[]>()
            {
                new [] {
                    new Context(SimerKey) {
                        MetricName = "pitwall_laptimes_seconds",
                        PilotName = "IntegrationTest_laptime",
                        SetFieldValue = t => t.LaptimeSeconds = 122.0,
                        GetApiModelInstance = () => ModelWithoutSubMappings(),
                        Expected = 122.0
                    }},
                 new [] {
                    new Context(SimerKey) {
                        MetricName = "pitwall_air_temperature_celsius",
                        PilotName = "IntegrationTest_airTemp",
                        SetFieldValue = t => t.AirTemperature= 10.0,
                        GetApiModelInstance = () => ModelWithoutSubMappings(),
                        Expected = 10.0
                    }},
                  new [] {
                    new Context(SimerKey) {
                        MetricName = "pitwall_road_wetness_avg_percent",
                        PilotName = "IntegrationTest_roadWetness",
                        SetFieldValue = t => t.AvgWetness= 0.3,
                        GetApiModelInstance = () => ModelWithoutSubMappings(),
                        Expected = 0.3
                    }},

                // ===================================================================

                new [] {
                    new Context(SimerKey) {
                        MetricName = "pitwall_tyres_temperatures_frontleft_celsius",
                        PilotName = "IntegrationTest_ttemp_frontleft",
                        SetFieldValue = t => t.TyresTemperatures.FrontLeftTemp = 60.0,
                        GetApiModelInstance = () => ModelWithTyreTemp(),
                        Expected = 60.0
                    }},

                new [] {
                    new Context(SimerKey) {
                        MetricName = "pitwall_tyres_temperatures_frontright_celsius",
                        PilotName = "IntegrationTest_ttemp_frontright",
                        SetFieldValue = t => t.TyresTemperatures.FrontRightTemp = 61.0,
                        GetApiModelInstance = () => ModelWithTyreTemp(),
                        Expected = 61.0
                    }},


                new [] {
                    new Context(SimerKey) {
                        MetricName = "pitwall_tyres_temperatures_rearleft_celsius",
                        PilotName = "IntegrationTest_ttemp_rearleft",
                        SetFieldValue = t => t.TyresTemperatures.FrontRightTemp = 62.0,
                        GetApiModelInstance = () => ModelWithTyreTemp(),
                        Expected = 62.0
                    }},

                  new [] {
                    new Context(SimerKey) {
                        MetricName = "pitwall_tyres_temperatures_rearright_celsius",
                        PilotName = "IntegrationTest_ttemp_rearright",
                        SetFieldValue = t => t.TyresTemperatures.FrontRightTemp = 63.0,
                        GetApiModelInstance = () => ModelWithTyreTemp(),
                        Expected = 63.0
                    }},
                };

                _inner = data;
            }

            private TelemetryModel ModelWithoutSubMappings()
            {
                return new TelemetryModel();
            }

            private TelemetryModel ModelWithTyreTemp()
            {
                return new TelemetryModel()
                {
                    TyresTemperatures = new TyresTemperatures()
                };
            }

            public IEnumerator<object[]> GetEnumerator()
            {
                return _inner.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _inner.GetEnumerator();
            }
        }

        [Theory]
        [ClassData(typeof(TestDataInstantMetrics))]
        public async void GIVEN_metric_THEN_read_from_timeSerie(IContext testContext)
        {

            Trace.WriteLine(nameof(GIVEN_metric_THEN_read_from_timeSerie) + " : " + testContext);
            Trace.WriteLine("");

            TelemetryModel model = testContext.GetApiModelInstance();

            model.SimerKey = testContext.SimerKey;

            model.PilotName = testContext.PilotName;

            testContext.SetFieldValue(model);

            {
                HttpResponseMessage response = await SendToApi(model);

                Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
            }

            //Thread.Sleep(16000);

            {
                string intermediary = await ReadInstantQueryResult(
                   testContext.MetricName,
                   PilotLabel,
                   testContext.PilotName);

                var actual = Double.Parse(intermediary, CultureInfo.InvariantCulture);

                Check.That(actual).IsEqualTo(testContext.Expected);
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

            client.BaseAddress = new Uri(TimeSerieUri);

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
            client.BaseAddress = new Uri(TargetApi);

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