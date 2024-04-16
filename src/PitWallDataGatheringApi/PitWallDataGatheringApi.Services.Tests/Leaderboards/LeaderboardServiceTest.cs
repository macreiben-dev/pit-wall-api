using NFluent;
using NSubstitute;
using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Repositories.Leaderboards;
using PitWallDataGatheringApi.Repositories.Leaderboards.Updates;
using PitWallDataGatheringApi.Services.Leaderboards;
using PitwallDataGatheringApi.Tests.BusinessCommon.Business;

namespace PitWallDataGatheringApi.Services.Tests.Leaderboards
{
    public partial class LeaderboardServiceTest
    {
        private readonly ILeaderboardRepository _repo;
        private readonly ILeaderboardLivetimingSqlRepository _liveRepo;
        private readonly ILeaderboardPitlaneRepository _pitlaneRepo;
        private readonly ILeaderboardInPitBoxRepository _inPitBox;

        private const string SomeOtherPilot = "some_other_pilot";
        private const string SomeOtherCarname = "some_other_carname";
        private const string CarClassGTE = "GTE";
        private const string CarNumber53 = "53";

        public LeaderboardServiceTest()
        {
            _repo = Substitute.For<ILeaderboardRepository>();

            _liveRepo = Substitute.For<ILeaderboardLivetimingSqlRepository>();

            _pitlaneRepo = Substitute.For<ILeaderboardPitlaneRepository>();

            _inPitBox = Substitute.For<ILeaderboardInPitBoxRepository>();
        }

        private ILeaderBoardService GetTarget()
        {
            return new LeaderboardService(_liveRepo, _pitlaneRepo, _inPitBox);
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
                    CarClass = CarClassGTE,
                    CarNumber = CarNumber53,
                    LastPitLap = 12,
                    Position = 13
                })
                .WithCar("some_car")
                .WithPilot("some_pilot");

            // ACT
            var target = GetTarget();

            target.Update(model);

            // ASSERT
            _liveRepo.Received(1)
                .Update(model);
        }

        [Fact]
        public void GIVEN_leaderboard_entry_AND_inPitlane_THEN_pitlane_gauge_one()
        {
            FakeLeaderboardModel model = new FakeLeaderboardModel()
                .AddEntry(BuildEntryInPitlane())
                .WithCar("some_car")
                .WithPilot("some_pilot");

            // ACT
            var target = GetTarget();

            target.Update(model);

            // ASSERT
            _pitlaneRepo.Received(1)
                .Update(new MetricData<double?>(
                    1.0,
                    "some_other_pilot",
                    SomeOtherCarname));
        }

        [Fact]
        public void GIVEN_leaderboard_entry_AND_notPitlane_THEN_pitlane_gauge_zero()
        {
            FakeLeaderboardModel model = new FakeLeaderboardModel()
                .AddEntry(BuildEntryNotInPitlane())
                .WithCar("some_car")
                .WithPilot("some_pilot");

            // ACT
            var target = GetTarget();

            target.Update(model);

            // ASSERT
            _pitlaneRepo.Received(1)
                .Update(new MetricData<double?>(
                    0.0,
                    "some_other_pilot",
                    SomeOtherCarname));
        }
        
        [Fact]
        public void GIVEN_leaderboard_entry_AND_inPitBox_THEN_inPitBox_gauge_one()
        {
            FakeLeaderboardModel model = new FakeLeaderboardModel()
                .AddEntry(BuildEntryInPitlane()
                    .WithIsInPitBox())
                .WithCar("some_car")
                .WithPilot("some_pilot");

            // ACT
            var target = GetTarget();

            target.Update(model);

            // ASSERT
            _inPitBox.Received(1)
                .Update(new MetricData<double?>(
                    1.0,
                    "some_other_pilot",
                    SomeOtherCarname));
        }
        
        private FakeBusinessEntry BuildEntryInPitlane()
        {
            return new FakeBusinessEntry()
            {
                CarClass = CarClassGTE,
                CarNumber = CarNumber53,
                LastPitLap = 12,
                Position = 13,
                PilotName = "some_other_pilot",
                CarName = SomeOtherCarname,
                InPitLane = true
            };
        }
        
        private FakeBusinessEntry BuildEntryNotInPitlane()
        {
            return new FakeBusinessEntry()
            {
                CarClass = CarClassGTE,
                CarNumber = CarNumber53,
                LastPitLap = 12,
                Position = 13,
                PilotName = "some_other_pilot",
                CarName = SomeOtherCarname,
                InPitLane = false
            };
        }

    }
}
