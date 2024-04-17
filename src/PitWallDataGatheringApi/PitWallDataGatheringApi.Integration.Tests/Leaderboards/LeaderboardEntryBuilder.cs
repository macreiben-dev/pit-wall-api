using PitWallDataGatheringApi.Integration.Tests.Leaderboards.ApiModel;

namespace PitWallDataGatheringApi.Integration.Tests.Leaderboards
{
    public sealed class LeaderboardEntryBuilder
    {
        private LeaderboardEntry _leaderboardEntry;

        public LeaderboardEntryBuilder()
        {
            _leaderboardEntry = new LeaderboardEntry();
        }

        public LeaderboardEntryBuilder WithLastPitLap(int lastPitLap)
        {
            _leaderboardEntry.LastPitLap = lastPitLap;

            return this;
        }

        public LeaderboardEntryBuilder WithCarClass(string carClass)
        {
            _leaderboardEntry.CarClass = carClass;

            return this;
        }

        public LeaderboardEntryBuilder WithCarNumber(string carNumber)
        {
            _leaderboardEntry.CarNumber = carNumber;

            return this;
        }

        public LeaderboardEntryBuilder WithPosition(int position)
        {
            _leaderboardEntry.Position = position;

            return this;
        }
        
        public LeaderboardEntryBuilder WithCarName(string carName)
        {
            _leaderboardEntry.CarName = carName;

            return this;
        }
        
        public LeaderboardEntry Build()
        {
            return _leaderboardEntry;
        }


    }
}
