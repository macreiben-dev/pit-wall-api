using PitWallDataGatheringApi.Integration.Tests.Frameworks;
using PitWallDataGatheringApi.Integration.Tests.Frameworks.ApiFramework;
using PitWallDataGatheringApi.Integration.Tests.Leaderboards.ApiModel;
using PitWallDataGatheringApi.Integration.Tests.PromFramework;

namespace PitWallDataGatheringApi.Integration.Tests.Leaderboards
{
    public class LeaderboardControllerSerieTest
    {
        private readonly Uri _leaderboardUri;
        private readonly string _connectionString;

        private const string JohnDoe = "John Doe";

        private const string IsInPitlaneSerieName = "pitwall_leaderboard_isinpitlane";
        private const string SimerKey = "some_test_looking_value23";

        private const string CarNumber_13 = "#0013 testing car leaderboard";

        private const string CarNumber_12 = "#0012 testing car leaderboard";
        private const string PilotName_12 = "Flynn Rider";
        
        private readonly RelativeUri leaderboardUri = new RelativeUri("api/v1/leaderboard");

        private IEnumerable<LeaderboardReadData> _actual;
        private readonly PrometheusMetricRepository _promRepository;
        private readonly ApiRepository _apiRepository;

        public LeaderboardControllerSerieTest()
        {
            _leaderboardUri = new Uri("http://localhost:32773");

            _promRepository = new PrometheusMetricRepository(new ServerAddress("http://localhost:10100"));

            _apiRepository = new ApiRepository(new ServerAddress("http://localhost:32773"));
        }

        [Fact]
        public async void Explore()
        {
            LeaderboardModel source = new LeaderboardModel(CarNumber_13, JohnDoe, SimerKey);

            source.Entries.Add(new LeaderboardEntry()
            {
                InPitLane = true,
                CarNumber = CarNumber_12,
            });

            await _apiRepository.SendToApi(source, leaderboardUri);

            Thread.Sleep(5000);

            var result = await _promRepository.ReadInstantQueryResult(
                IsInPitlaneSerieName,
                "Car",
                CarNumber_13);

            // InstantMetricReadLeaderboard.GIVEN_metric_THEN_read_from_timeSerie(source)
        }
    }
}