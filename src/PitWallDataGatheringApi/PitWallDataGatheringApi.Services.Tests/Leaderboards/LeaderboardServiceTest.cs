using NFluent;
using NSubstitute;
using PitWallDataGatheringApi.Models.Business.Leaderboards;
using PitWallDataGatheringApi.Repositories.Leaderboards;
using PitWallDataGatheringApi.Services.Leaderboards;

namespace PitWallDataGatheringApi.Services.Tests.Leaderboards
{
    public partial class LeaderboardServiceTest
    {
        private ILeaderboardCarNumberRepository _carNumberMetricRepo;

        public LeaderboardServiceTest()
        {

            _carNumberMetricRepo = Substitute.For<ILeaderboardCarNumberRepository>();
        }

        private ILeaderBoardService GetTarget()
        {
            return new LeaderboardService(_carNumberMetricRepo);
        }

        [Fact]
        public void GIVEN_model_isNull_THEN_fail()
        {
            Check.ThatCode(() => GetTarget().Update(null))
                .Throws<ArgumentNullException>();
        }

        [Fact]
        public void GIVEN_model_with_oneEntry_THEN_persistData()
        {
            FakeLeaderboardModel model = new FakeLeaderboardModel()
                .AddEntry(new FakeEntry()
                {
                    CarClass = "GTE",
                    CarNumber = 53,
                    LastPitLap = 12,
                    Position = 13
                })
                .WithCar("some_car")
                .WithPilot("some_pilot");

            // ACT
            var target = GetTarget();

            target.Update(model);

            // ASSERT
            _carNumberMetricRepo.Received(1)
                .Update(Arg.Is<ILeaderboardEntry>(arg => arg.CarClass == "GTE"),
                    new Models.PilotName("some_pilot"),
                    new Models.CarName("some_car"));
        }
    }
}
