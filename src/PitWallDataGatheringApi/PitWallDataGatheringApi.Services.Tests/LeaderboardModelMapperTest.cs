using ApiLeaderboardModel = PitWallDataGatheringApi.Models.Apis.v1.Leaderboards.LeaderboardModel;
using ApiLeaderboardEntry = PitWallDataGatheringApi.Models.Apis.v1.Leaderboards.LeaderboardEntry;

using BusinessLeaderboardModel = PitWallDataGatheringApi.Models.Business.Leaderboards.LeaderboardModel;
using BusinessLeaderboardEntry = PitWallDataGatheringApi.Models.Business.Leaderboards.LeaderboardEntry;
using NFluent;

namespace PitWallDataGatheringApi.Services.Tests
{
    public sealed class LeaderboardModelMapperTest
    {
        private const int LastPitLapValue = 4;
        private const string CarClassValue = "GTE";
        private const int CarNumberValue = 13;
        private const int PositionValue = 5;
        private ApiLeaderboardModel _source;

        public LeaderboardModelMapperTest()
        {
            ApiLeaderboardModel source = new ApiLeaderboardModel();

            source.Entries = new List<ApiLeaderboardEntry>();

            source.Entries.Add(new ApiLeaderboardEntry()
            {
                CarClass = CarClassValue,
                CarNumber = CarNumberValue,
                CarName = "SomeCarName",
                LastLapInSeconds = 122.0,
                PitCount = 3,
                LastPitLap = LastPitLapValue,
                Position = PositionValue,
                LastSector1InSeconds = 123.3,
                LastSector2InSeconds = 124.4,
                LastSector3InSeconds = 125.5,
            });

            _source = source;
        }

        private LeaderboardModelMapper GetTarget()
        {
            return new LeaderboardModelMapper();
        }

        [Fact]
        public void GIVEN_entry_THEN_map_position()
        {
            var actual = GetTarget().Map(_source);

            var intermediary = actual.FirstOrDefault(c => c.LastPitLap == LastPitLapValue);

            Check.That(intermediary).IsNotNull();
        }

        [Fact]
        public void GIVEN_entry_THEN_map_carClass()
        {
            var actual = GetTarget().Map(_source);

            var intermediary = actual.FirstOrDefault(c => c.CarClass == CarClassValue);

            Check.That(intermediary).IsNotNull();
        }

        [Fact]
        public void GIVEN_entry_THEN_map_carNumber()
        {
            var actual = GetTarget().Map(_source);

            var intermediary = actual.FirstOrDefault(c => c.CarNumber == CarNumberValue);

            Check.That(intermediary).IsNotNull();
        }

        [Fact]
        public void GIVEN_entry_THEN_map_carPosition()
        {
            var actual = GetTarget().Map(_source);

            var intermediary = actual.FirstOrDefault(c => c.Position == PositionValue);

            Check.That(intermediary).IsNotNull();
        }
    }
}
