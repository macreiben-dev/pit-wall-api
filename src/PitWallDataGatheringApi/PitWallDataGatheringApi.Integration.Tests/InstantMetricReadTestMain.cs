using Newtonsoft.Json;
using NFluent;
using PitWallDataGatheringApi.Models.Apis;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace PitWallDataGatheringApi.Integration.Tests
{
    internal static class InstantMetricReadTestMain
    {
        private const string PilotLabel = "Pilot";

        public static void GIVEN_metric_THEN_read_from_timeSerie(IContext testContext, string targetApi, string timeSerieUri)
        {

            Trace.WriteLine(nameof(GIVEN_metric_THEN_read_from_timeSerie) + " : " + testContext);
            Trace.WriteLine("");

            TelemetryModel model = testContext.GetApiModelInstance();

            model.SimerKey = testContext.SimerKey;

            model.PilotName = testContext.PilotName;
            
            model.CarName = testContext.CarName;

            testContext.SetFieldValue(model);

            {
                Task<HttpResponseMessage> curent = InstantMetricReadTestHelpers.SendToApi(model, targetApi);

                Task.WaitAll(curent);

                HttpResponseMessage responseMessage = curent.Result;

                if(responseMessage.StatusCode != HttpStatusCode.OK)
                {
                    DisplayContent(responseMessage);
                }

                Check.That(responseMessage.StatusCode).IsEqualTo(HttpStatusCode.OK);
            }

            Thread.Sleep(5000);

            {
                /**
                 * Idea: Use retry every seconds for 5 seconds method
                 * */

                Task<string> readPilot = InstantMetricReadTestHelpers.ReadInstantQueryResult(
                   testContext.MetricName,
                   PilotLabel,
                   testContext.PilotName,
                   timeSerieUri);

                Task<string> readCar = InstantMetricReadTestHelpers.ReadInstantQueryResult(
                   testContext.MetricName,
                   "Car",
                   testContext.CarName,
                   timeSerieUri);

                ExecuteAndAssert(readPilot, testContext.Expected);

                ExecuteAndAssert(readCar, testContext.Expected);
            }
        }

        private static void DisplayContent(HttpResponseMessage responseMessage)
        {
            using StreamReader reader = new StreamReader(responseMessage.Content.ReadAsStream());

            string response = reader.ReadToEnd();

            var x = JsonConvert.DeserializeObject(response);

            var formated = JsonConvert.SerializeObject(x, Formatting.Indented);

            Trace.WriteLine(formated.ToString());
        }

        private static void ExecuteAndAssert(Task<string> task1, object expected)
        {
            Task.WaitAll(task1);

            string intermediary = ReadResult(task1);

            var actual = Double.Parse(intermediary, CultureInfo.InvariantCulture);

            Check.That(actual).IsEqualTo(expected);
        }

        private static string ReadResult(Task<string> result)
        {
            if (result.Exception != null)
            {
                var inner = result.Exception.InnerExceptions.FirstOrDefault();

                if (inner != null)
                {
                    throw inner;
                }

                throw result.Exception;
            }

            string intermediary = result.Result;
            return intermediary;
        }
    }
}