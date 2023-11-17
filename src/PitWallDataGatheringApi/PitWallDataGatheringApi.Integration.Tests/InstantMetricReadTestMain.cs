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

            testContext.SetFieldValue(model);

            {
                Task<HttpResponseMessage> curent = InstantMetricReadTestHelpers.SendToApi(model, targetApi);

                Task.WaitAll(curent);

                HttpResponseMessage responseMessage = curent.Result;

                Check.That(responseMessage.StatusCode).IsEqualTo(HttpStatusCode.OK);
            }

            Thread.Sleep(5000);

            {
                /**
                 * Idea: Use retry every seconds for 5 seconds method
                 * */

                Task<string> read = InstantMetricReadTestHelpers.ReadInstantQueryResult(
                   testContext.MetricName,
                   PilotLabel,
                   testContext.PilotName,
                   timeSerieUri);

                Task.WaitAll(read);
                
                if(read.Exception != null )
                {
                    var inner = read.Exception.InnerExceptions.FirstOrDefault();

                    if(inner != null)
                    {
                        throw inner;
                    }

                    throw read.Exception;
                }

                string intermediary = read.Result;

                var actual = Double.Parse(intermediary, CultureInfo.InvariantCulture);

                Check.That(actual).IsEqualTo(testContext.Expected);
            }
        }
    }
}