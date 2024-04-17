using NFluent;
using PitWallDataGatheringApi.Models.Apis.v1;
using System.Diagnostics;
using System.Net;
using PitWallDataGatheringApi.Integration.Tests.PromFramework;

namespace PitWallDataGatheringApi.Integration.Tests.Telemetries
{
    internal static class InstantMetricReadTestMain
    {
        private const string PilotLabel = "Pilot";

        public static void GIVEN_metric_THEN_read_from_timeSerie(ITelemetryContext testContext, string targetApi, string timeSerieUri)
        {

            Trace.WriteLine(nameof(GIVEN_metric_THEN_read_from_timeSerie) + " : " + testContext);
            Trace.WriteLine("");

            TelemetryModel model = testContext.GetApiModelInstance();

            model.SimerKey = testContext.SimerKey;

            model.PilotName = testContext.PilotName;

            model.CarName = testContext.CarName;

            testContext.SetFieldValue(model);

            {
                Task<HttpResponseMessage> curent = InstantMetricReadTestHelpers.SendToApi(model, targetApi, "/api/v1/Telemetry");

                Task.WaitAll(curent);

                HttpResponseMessage responseMessage = curent.Result;

                if (responseMessage.StatusCode != HttpStatusCode.OK)
                {
                    TestHelper.DisplayContent(responseMessage);
                }

                Check.That(responseMessage.StatusCode).IsEqualTo(HttpStatusCode.OK);
            }

            Thread.Sleep(5000);

            {
                /**
                 * Idea: Use retry every seconds for 5 seconds method
                 * */

                Task<string?> readPilot = InstantMetricReadTestHelpers.ReadInstantQueryResult(
                   testContext.MetricName,
                   PilotLabel,
                   testContext.PilotName,
                   timeSerieUri);

                Task<string?> readCar = InstantMetricReadTestHelpers.ReadInstantQueryResult(
                   testContext.MetricName,
                   "Car",
                   testContext.CarName,
                   timeSerieUri);

                TestHelper.ExecuteAndAssert(readPilot, testContext.Expected);

                TestHelper.ExecuteAndAssert(readCar, testContext.Expected);
            }
        }
    }
}