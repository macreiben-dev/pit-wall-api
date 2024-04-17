using NFluent;
using PitWallDataGatheringApi.Integration.Tests.Frameworks;
using PitWallDataGatheringApi.Integration.Tests.Frameworks.ApiFramework;
using PitWallDataGatheringApi.Integration.Tests.Leaderboards.ApiModel;
using PitWallDataGatheringApi.Integration.Tests.PromFramework;

namespace PitWallDataGatheringApi.Integration.Tests.Leaderboards
{
    public class LeaderboardControllerSerieTest
    {
        private const string JohnDoe = "John Doe";

        private const string IsInPitlaneSerieName = "pitwall_leaderboard_isinpitlane";
        private const string SimerKey = "some_test_looking_value23";

        private const string CarNumber13 = "#0013 pilot sending data to leaderboard";

        private const string CarNumber12 = "#0012 testing car leaderboard";
        private const string CarNumber11 = "#0011 testing car leaderboard";

        private readonly RelativeUri _leaderboardUri = new("api/v1/leaderboard");

        private IEnumerable<LeaderboardReadData> _actual;
        private readonly PrometheusMetricRepository _promRepository;
        private readonly ApiRepository _apiRepository;

        public LeaderboardControllerSerieTest()
        {
            _promRepository = new PrometheusMetricRepository(new ServerAddress("http://localhost:10100"));

            _apiRepository = new ApiRepository(new ServerAddress("http://localhost:32773"));
        }

        [Fact]
        public async void GIVEN_in_pitlane_THEN_matchedCarName_returns_1()
        {
            LeaderboardModel source = new LeaderboardModel(
                CarNumber13, 
                JohnDoe, 
                SimerKey);

            source.Entries.Add(new LeaderboardEntry()
            {
                InPitLane = true,
                CarName = CarNumber12,
            });

            await _apiRepository.SendToApi(source, _leaderboardUri);

            Wait();

            var result = await _promRepository.ReadInstantQueryResult(
                IsInPitlaneSerieName,
                "Car",
                CarNumber12);

            Check.That(result).IsEqualTo("1");
        }

        [Fact]
        public async void GIVEN_not_in_pitlane_THEN_matchedCarName_returns_0()
        {
            LeaderboardModel source = new LeaderboardModel(CarNumber13, JohnDoe, SimerKey);

            source.Entries.Add(new LeaderboardEntry()
            {
                InPitLane = false,
                CarName = CarNumber11,
            });

            await _apiRepository.SendToApi(source, _leaderboardUri);

            Wait();

            var result = await _promRepository.ReadInstantQueryResult(
                IsInPitlaneSerieName,
                "Car",
                CarNumber11);

            Check.That(result).IsEqualTo("0");
        }

        private static void Wait()
        {
            Thread.Sleep(5000);
        }
    }
}