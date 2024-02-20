using Newtonsoft.Json;
using NFluent;
using PitWallDataGatheringApi.Integration.Tests.Leaderboards;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace PitWallDataGatheringApi.Integration.Tests
{
    public partial class InstantMetricLeaderboardReadTest
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
                        CarNumber = "53",
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
                       TestHelper.DisplayContent(responseMessage);
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

                    TestHelper.ExecuteAndAssert(readPilot, testContext.MetricValueToAssert);

                    TestHelper.ExecuteAndAssert(readCar, testContext.MetricValueToAssert);
                }
            }
        }
    }
}
