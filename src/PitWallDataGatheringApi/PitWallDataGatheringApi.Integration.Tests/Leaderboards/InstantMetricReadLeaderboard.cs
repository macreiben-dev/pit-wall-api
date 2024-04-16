using System.Diagnostics;
using System.Net;
using NFluent;
using PitWallDataGatheringApi.Integration.Tests.Leaderboards.ApiModel;

namespace PitWallDataGatheringApi.Integration.Tests.Leaderboards;

public class InstantMetricReadLeaderboard
{
    private const string TimeSerieUri = "http://localhost:10100";
    private const string TargetApi = "http://localhost:32773";
    private const string SimerKey = "some_test_looking_value23";
    private const string PilotLabel = "Pilot";
    private const string CarLabel = "Car";

    public static Models.Apis.v1.Leaderboards.LeaderboardModel LeaderboardModel()
    {
        return new Models.Apis.v1.Leaderboards.LeaderboardModel();
    }
    
    public static void GIVEN_metric_THEN_read_from_timeSerie(Models.Apis.v1.Leaderboards.LeaderboardModel model, string targetApi, string timeSerieUri)
    {

        Trace.WriteLine(nameof(GIVEN_metric_THEN_read_from_timeSerie) + " : " + model);
        Trace.WriteLine("");

        {
            Task<HttpResponseMessage> curent = InstantMetricReadTestHelpers.SendToApi(
                model,
                targetApi,
                "/api/v1/Leaderboard");

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
                CarLabel,
                testContext.CarName,
                timeSerieUri);

            TestHelper.ExecuteAndAssert(readPilot, testContext.Expected);

            TestHelper.ExecuteAndAssert(readCar, testContext.Expected);
        }
    }
}