using Microsoft.Extensions.Logging;
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
    public class LeaderboardServiceTest
    {
        private readonly ILeaderboardLivetimingSqlRepository _liveRepo;
        private readonly ILeaderboardPitlaneRepository _pitlaneRepo;
        private readonly ILeaderboardInPitBoxRepository _inPitBox;
        private readonly ILogger<LeaderboardService> _logger;

        private const string LeaderboardEntryPilotName = "some_other_pilotname";
        private const string LeaderboardEntryCarName = "some_other_carname";
        private const string CarClassGte = "GTE";
        private const string CarNumber53 = "53";
        
        private const string SourceCarName = "source_carname";  
        private const string SourcePilotName = "source_pilotname";  

        public LeaderboardServiceTest()
        {
            _liveRepo = Substitute.For<ILeaderboardLivetimingSqlRepository>();

            _pitlaneRepo = Substitute.For<ILeaderboardPitlaneRepository>();

            _inPitBox = Substitute.For<ILeaderboardInPitBoxRepository>();

            _logger = Substitute.For<ILogger<LeaderboardService>>();
        }

        private ILeaderBoardService GetTarget()
        {
            return new LeaderboardService(_liveRepo, _pitlaneRepo, _inPitBox, _logger);
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
                    CarClass = CarClassGte,
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
                .WithCar(SourceCarName)
                .WithPilot(SourcePilotName);

            // ACT
            var target = GetTarget();

            target.Update(model);

            // ASSERT
            _pitlaneRepo.Received(1)
                .Update(new MetricDataWithSource<double?>(
                    1.0,
                    new PilotName(LeaderboardEntryPilotName),
                    new CarName(LeaderboardEntryCarName),
                    new PilotName(SourcePilotName),
                    new CarName(SourceCarName)));
        }

        [Fact]
        public void GIVEN_leaderboard_entry_AND_notPitlane_THEN_pitlane_gauge_zero()
        {
            FakeLeaderboardModel model = new FakeLeaderboardModel()
                .AddEntry(BuildEntryNotInPitlane())
                .WithCar(SourceCarName)
                .WithPilot(SourcePilotName);

            // ACT
            var target = GetTarget();

            target.Update(model);

            // ASSERT
            _pitlaneRepo.Received(1)
                .Update(new MetricDataWithSource<double?>(
                    0.0,
                    new PilotName(LeaderboardEntryPilotName),
                    new CarName(LeaderboardEntryCarName),
                    new PilotName(SourcePilotName),
                    new CarName(SourceCarName))
                );
        }

        [Fact]
        public void GIVEN_leaderboard_entry_AND_inPitBox_THEN_inPitBox_gauge_one()
        {
            FakeLeaderboardModel model = new FakeLeaderboardModel()
                .AddEntry(BuildEntryInPitlane()
                    .WithIsInPitBox())
                .WithCar(SourceCarName)
                .WithPilot(SourcePilotName);

            // ACT
            var target = GetTarget();

            target.Update(model);

            // ASSERT
            _inPitBox.Received(1)
                .Update(new MetricDataWithSource<double?>(
                    1.0,
                    new PilotName(LeaderboardEntryPilotName),
                    new CarName(LeaderboardEntryCarName),
                    new PilotName(SourcePilotName),
                    new CarName(SourceCarName)));
        }

        [Fact]
        public void GIVEN_clear_is_called_THEN_clear_called_on_repo()
        {
            GetTarget().ClearLiveTiming();
            
            _liveRepo.Received(1).Clear();
        }
        
        private FakeBusinessEntry BuildEntryInPitlane()
        {
            return new FakeBusinessEntry()
            {
                CarClass = CarClassGte,
                CarNumber = CarNumber53,
                LastPitLap = 12,
                Position = 13,
                PilotName = LeaderboardEntryPilotName,
                CarName = LeaderboardEntryCarName,
                InPitLane = true
            };
        }

        private FakeBusinessEntry BuildEntryNotInPitlane()
        {
            return new FakeBusinessEntry()
            {
                CarClass = CarClassGte,
                CarNumber = CarNumber53,
                LastPitLap = 12,
                Position = 13,
                PilotName = LeaderboardEntryPilotName,
                CarName = LeaderboardEntryCarName,
                InPitLane = false
            };
        }
    }
}