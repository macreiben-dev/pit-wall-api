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
        private const string SimerKey = "some_test_looking_value23";

        [Fact]
        public void GIVEN_pitwall_tyres_wear_rearleft_percent_THEN_read_from_timeSerie()
        {
            var context = new Context(SimerKey)
            {
                MetricName = "pitwall_tyres_wear_rearleft_percent",
                PilotName = "IntegrationTest_twear_rearleft",
                SetFieldValue = t => t.TyresWear.ReartLeftWear = 60.0,
                GetApiModelInstance = () => ModelWithTyreWear(),
                Expected = 60.0
            };

            GIVEN_metric_THEN_read_from_timeSerie(context);
        }

        [Fact]
        public void GIVEN_pitwall_tyres_wear_frontright_percent_THEN_read_from_timeSerie()
        {
            var context = new Context(SimerKey)
            {
                MetricName = "pitwall_tyres_wear_frontright_percent",
                PilotName = "IntegrationTest_twear_frontright",
                SetFieldValue = t => t.TyresWear.FrontRightWear = 61.0,
                GetApiModelInstance = () => ModelWithTyreWear(),
                Expected = 61.0
            };

            GIVEN_metric_THEN_read_from_timeSerie(context);
        }

        [Fact]
        public void GIVEN_pitwall_tyres_wear_frontleft_percent_THEN_read_from_timeSerie()
        {
            var context = new Context(SimerKey)
            {
                MetricName = "pitwall_tyres_wear_frontleft_percent",
                PilotName = "IntegrationTest_twear_frontleft",
                SetFieldValue = t => t.TyresWear.FrontLeftWear = 60.0,
                GetApiModelInstance = () => ModelWithTyreWear(),
                Expected = 60.0
            };

            GIVEN_metric_THEN_read_from_timeSerie(context);
        }

        // =============

        [Fact]
        public void GIVEN_pitwall_tyres_temperatures_rearright_celsius_THEN_read_from_timeSerie()
        {
            var context = new Context(SimerKey)
            {
                MetricName = "pitwall_tyres_temperatures_rearright_celsius",
                PilotName = "IntegrationTest_ttemp_rearright",
                SetFieldValue = t => t.TyresTemperatures.RearRightTemp = 62.0,
                GetApiModelInstance = () => ModelWithTyreTemp(),
                Expected = 62.0
            };

            GIVEN_metric_THEN_read_from_timeSerie(context);
        }

        [Fact]
        public void GIVEN_pitwall_tyres_temperatures_rearleft_celsius_THEN_read_from_timeSerie()
        {
            var context = new Context(SimerKey)
            {
                MetricName = "pitwall_tyres_temperatures_rearleft_celsius",
                PilotName = "IntegrationTest_ttemp_rearleft",
                SetFieldValue = t => t.TyresTemperatures.RearLeftTemp = 62.0,
                GetApiModelInstance = () => ModelWithTyreTemp(),
                Expected = 62.0
            };

            GIVEN_metric_THEN_read_from_timeSerie(context);
        }

        [Fact]
        public void GIVEN_pitwall_tyres_temperatures_frontright_celsius_THEN_read_from_timeSerie()
        {
            var context = new Context(SimerKey)
            {
                MetricName = "pitwall_tyres_temperatures_frontright_celsius",
                PilotName = "IntegrationTest_ttemp_frontright",
                SetFieldValue = t => t.TyresTemperatures.FrontRightTemp = 61.0,
                GetApiModelInstance = () => ModelWithTyreTemp(),
                Expected = 61.0
            };

            GIVEN_metric_THEN_read_from_timeSerie(context);
        }

        [Fact]
        public void GIVEN_pitwall_tyres_temperatures_frontleft_celsius_THEN_read_from_timeSerie()
        {
            var context = new Context(SimerKey)
            {
                MetricName = "pitwall_tyres_temperatures_frontleft_celsius",
                PilotName = "IntegrationTest_ttemp_frontleft",
                SetFieldValue = t => t.TyresTemperatures.FrontLeftTemp = 60.0,
                GetApiModelInstance = () => ModelWithTyreTemp(),
                Expected = 60.0
            };

            GIVEN_metric_THEN_read_from_timeSerie(context);
        }

        [Fact]
        public void GIVEN_pitwall_road_wetness_avg_percent_THEN_read_from_timeSerie()
        {
            var context = new Context(SimerKey)
            {
                MetricName = "pitwall_road_wetness_avg_percent",
                PilotName = "IntegrationTest_roadWetness",
                SetFieldValue = t => t.AvgWetness = 0.3,
                GetApiModelInstance = () => ModelWithoutSubMappings(),
                Expected = 0.3
            };

            GIVEN_metric_THEN_read_from_timeSerie(context);
        }

        [Fact]
        public void GIVEN_pitwall_laptimes_seconds_THEN_read_from_timeSerie()
        {
            var context = new Context(SimerKey)
            {
                MetricName = "pitwall_laptimes_seconds",
                PilotName = "IntegrationTest_laptime",
                SetFieldValue = t => t.LaptimeSeconds = 122.0,
                GetApiModelInstance = () => ModelWithoutSubMappings(),
                Expected = 122.0
            };

            GIVEN_metric_THEN_read_from_timeSerie(context);
        }

        [Fact]
        public void GIVEN_pitwall_air_temperature_celsius_THEN_read_from_timeSerie()
        {
            var context = new Context(SimerKey)
            {
                MetricName = "pitwall_air_temperature_celsius",
                PilotName = "IntegrationTest_airTemp",
                SetFieldValue = t => t.AirTemperature = 10.0,
                GetApiModelInstance = () => ModelWithoutSubMappings(),
                Expected = 10.0
            };

            GIVEN_metric_THEN_read_from_timeSerie(context);
        }

        private void GIVEN_metric_THEN_read_from_timeSerie(IContext testContext)
        {

            Trace.WriteLine(nameof(GIVEN_metric_THEN_read_from_timeSerie) + " : " + testContext);
            Trace.WriteLine("");

            TelemetryModel model = testContext.GetApiModelInstance();

            model.SimerKey = testContext.SimerKey;

            model.PilotName = testContext.PilotName;

            testContext.SetFieldValue(model);

            {
                Task<HttpResponseMessage> curent = SendToApi(model);

                Task.WaitAll(curent);

                HttpResponseMessage responseMessage = curent.Result;

                Check.That(responseMessage.StatusCode).IsEqualTo(HttpStatusCode.OK);
            }

            Thread.Sleep(4000);

            {
                /**
                 * Idea: Use retry every seconds for 5 seconds method
                 * */

                Task<string> read = ReadInstantQueryResult(
                   testContext.MetricName,
                   PilotLabel,
                   testContext.PilotName);

                Task.WaitAll(read);

                string intermediary = read.Result;

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

            if (json == null)
            {
                throw new NoResultException(TimeSerieUri, queryPath);
            }

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

            var response = await client.PostAsync("/api/v1/Telemetry", content);
            return response;
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


        private TelemetryModel ModelWithTyreWear()
        {
            return new TelemetryModel()
            {
                TyresWear = new TyresWear()
            };
        }
    }
}