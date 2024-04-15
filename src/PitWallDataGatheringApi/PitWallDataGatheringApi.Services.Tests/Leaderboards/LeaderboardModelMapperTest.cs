using ApiLeaderboardModel = PitWallDataGatheringApi.Models.Apis.v1.Leaderboards.LeaderboardModel;
using ApiLeaderboardEntry = PitWallDataGatheringApi.Models.Apis.v1.Leaderboards.LeaderboardEntry;
using NFluent;
using PitWallDataGatheringApi.Services.Leaderboards;
using PitWallDataGatheringApi.Models;

namespace PitWallDataGatheringApi.Services.Tests.Leaderboards
{
    public sealed class LeaderboardModelMapperTest
    {
        private const int LastPitLapValue = 4;
        private const string CarClassValue = "GTE";
        private const string CarNumberValue = "13";
        private const int PositionValue = 5;
        private const string PilotName = "somePilotName";
        private const string CarName = "someCarName";
        private ApiLeaderboardModel _source;

        public LeaderboardModelMapperTest()
        {
            ApiLeaderboardModel source = new ApiLeaderboardModel();

            source.PilotName = PilotName;

            source.CarName = CarName;

            source.Entries = new List<ApiLeaderboardEntry>
            {
                new ApiLeaderboardEntry()
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
                }
            };

            _source = source;
        }

        private LeaderboardModelMapper GetTarget()
        {
            return new LeaderboardModelMapper();
        }

        [Fact]
        public void GIVEN_pilotName_THEN_map()
        {
            var expected = new PilotName(PilotName);

            var actual = GetTarget().Map(_source);

            Check.That(actual.PilotName).IsEqualTo(expected);
        }

        [Fact]
        public void GIVEN_carName_THEN_map()
        {
            var expected = new CarName(CarName);

            var actual = GetTarget().Map(_source);

            Check.That(actual.CarName).IsEqualTo(expected);
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

        [Fact]
        public void GIVEN_carClass_isNull_THEN_map_NA()
        {
            _source.Entries.First().CarClass = null;

            var actual = GetTarget().Map(_source);

            Check.That(actual.First().CarClass).IsEqualTo("NA");
        }

        [Fact]
        public void GIVEN_inPitlane_is_true_THEN_map()
        {
            _source.Entries.First().InPitLane = true;

            var actual = GetTarget().Map(_source);

            Check.That(actual.First().InPitLane).IsTrue();
        }
    }
}
