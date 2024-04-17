using System.Diagnostics;
using System.Net;
using NFluent;
using PitWallDataGatheringApi.Integration.Tests.Frameworks.PromFramework;
using PitWallDataGatheringApi.Integration.Tests.Leaderboards.ApiModel;
using LeaderboardModel = PitWallDataGatheringApi.Models.Apis.v1.Leaderboards.LeaderboardModel;

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

    public static void GIVEN_metric_THEN_read_from_timeSerie(LeaderboardModel model,
        string timeSerieUri,
        string metricName,
        string expected,
        LabelValues labelValues)
    {

        Trace.WriteLine(nameof(GIVEN_metric_THEN_read_from_timeSerie) + " : " + model);
        Trace.WriteLine("");

        {
            Task<HttpResponseMessage> curent = InstantMetricReadTestHelpers.SendToApi(
                model,
                TargetApi,
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
            Task<string?> readPilot = InstantMetricReadTestHelpers.ReadInstantQueryResult(
                metricName,
                PilotLabel,
                labelValues.Pilot.ToString(),
                timeSerieUri);

            Task<string?> readCar = InstantMetricReadTestHelpers.ReadInstantQueryResult(
                metricName,
                CarLabel,
                labelValues.Car.ToString(),
                timeSerieUri);

            TestHelper.ExecuteAndAssert(readPilot, expected);

            TestHelper.ExecuteAndAssert(readCar, expected);
        }
    }
}