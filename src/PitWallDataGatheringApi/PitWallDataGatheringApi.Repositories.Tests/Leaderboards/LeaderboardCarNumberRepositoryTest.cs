using NSubstitute;
using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Models.Business.Leaderboards;
using PitWallDataGatheringApi.Repositories.Leaderboards;
using PitWallDataGatheringApi.Repositories.Prom;
using System.Reflection.Metadata;

namespace PitWallDataGatheringApi.Repositories.Tests.LeaderboardCarNumberRepositories
{
    public sealed class LeaderboardCarNumberRepositoryTest
    {
        private IGaugeFactory _gaugeFactory;
        private IGauge _gauge;
        private const string LocalSerieNameFormat = "pitwall_leaderboard_position{0}_carnumber";
        private const string LocalDescrptionFormat = "Entry information for position {0}.";
        private const int EntryPosition = 13;

        private const string PilotNameValue = "somePilotName";
        private const string CarNameValue = "someCarName";

        public LeaderboardCarNumberRepositoryTest()
        {
            _gaugeFactory = Substitute.For<IGaugeFactory>();

            _gauge = Substitute.For<IGauge>();
        }

        private LeaderboardCarNumberRepository GetTarget()
        {
            return new LeaderboardCarNumberRepository(_gaugeFactory);
        }

        [Fact]
        public void GIVEN_entry_AND_pilotName_AND_carName_THEN_update_gauge()
        {
            // ARRANGE
            _gaugeFactory.CreateLeaderboardGauge(
                "pitwall_leaderboard_position{0}_carnumber",
                "Entry information for position {0}.",
                EntryPosition,
                ConstantLabels.Labels)
                .Returns(_gauge);

            var entry = new FakeEntry()
            {

                CarClass = "GTE",
                CarNumber = 53,
                LastPitLap = 12,
                Position = EntryPosition
            };

            // ACT
            var target = GetTarget();

            target.Update(entry, new PilotName(PilotNameValue), new CarName(CarNameValue));

            // ASSERT

            _gauge.Received(1).Update(
                Arg.Is<string[]>(arg => arg.Contains(PilotNameValue) && arg.Contains(CarNameValue)),
                53.0);
        }
    }

    internal class FakeEntry : ILeaderboardEntry
    {
        public int LastPitLap { get; set; }

        public string CarClass { get; set; }

        public int CarNumber { get; set; }

        public int Position { get; set; }
    }
}
