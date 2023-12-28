using Newtonsoft.Json;
using NFluent;
using PitWallDataGatheringApi.Integration.Tests.Leaderboards;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace PitWallDataGatheringApi.Integration.Tests
{
    public partial class InstantMetricReadTest
    {
        public class LeadboardCarNumber
        {
            [Fact]
            public void GIVEN_carNumber_THEN_readCardNumber()
            {
                LeaderboardContext context = new LeaderboardContext()
                    .WithCarName("CarNumber_CarName")
                    .WithPilotName("CarNumber_Pilot")
                    .Add(new Models.Apis.v1.Leaderboards.LeaderboardEntry()
                    {
                        CarClass = "GTE",
                        CarName = "some_CarNumber",
                        CarNumber = 53,
                        Position = 22
                    })
                    .ShouldAssertMetric("pitwall_leaderboard_position22_carnumber", 53.0);

                GIVEN_metric_THEN_read_from_timeSerie(context);
            }

            private const string PilotLabel = "Pilot";
            private const string CarLabel = "Car";

            public static void GIVEN_metric_THEN_read_from_timeSerie(
                LeaderboardContext testContext)
            {
                Trace.WriteLine(nameof(GIVEN_metric_THEN_read_from_timeSerie) + " : " + testContext);
                Trace.WriteLine("");

                var model = testContext.AsApiModel();

                {
                    Task<HttpResponseMessage> curent = InstantMetricReadTestHelpers.SendToApi(
                        model,
                        testContext.TargetApi,
                        testContext.RequestUri);

                    Task.WaitAll(curent);

                    HttpResponseMessage responseMessage = curent.Result;

                    if (responseMessage.StatusCode != HttpStatusCode.OK)
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
                       testContext.MetricNameToAssert,
                       PilotLabel,
                       testContext.PilotName,
                      testContext.TimeSerieUri);

                    Task<string> readCar = InstantMetricReadTestHelpers.ReadInstantQueryResult(
                       testContext.MetricNameToAssert,
                       CarLabel,
                       testContext.CarName,
                       testContext.TimeSerieUri);

                    ExecuteAndAssert(readPilot, testContext.MetricValueToAssert);

                    ExecuteAndAssert(readCar, testContext.MetricValueToAssert);
                }
            }

            // ================ NOT MODIFIED ================

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

            // ================ NOT MODIFIED ================
        }
    }
}
