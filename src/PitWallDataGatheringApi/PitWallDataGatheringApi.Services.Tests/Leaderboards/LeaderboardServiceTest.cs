﻿using NFluent;
using NSubstitute;
using PitWallDataGatheringApi.Repositories.Leaderboards;
using PitWallDataGatheringApi.Services.Leaderboards;

namespace PitWallDataGatheringApi.Services.Tests.Leaderboards
{
    public partial class LeaderboardServiceTest
    {
        private ILeaderboardRepository _repo;
        private ILeaderboardLivetimingSqlRepository _liveRepo;

        public LeaderboardServiceTest()
        {

            _repo = Substitute.For<ILeaderboardRepository>();

            _liveRepo = Substitute.For<ILeaderboardLivetimingSqlRepository>();
        }

        private ILeaderBoardService GetTarget()
        {
            return new LeaderboardService(_repo, _liveRepo);
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
                .AddEntry(new FakeBusinessEntry()
                {
                    CarClass = "GTE",
                    CarNumber = "53",
                    LastPitLap = 12,
                    Position = 13
                })
                .WithCar("some_car")
                .WithPilot("some_pilot");

            // ACT
            var target = GetTarget();

            target.Update(model);

            // ASSERT
            _repo.Received(1)
                .Update(model);
        }
    }
}
