using PitWallDataGatheringApi.Integration.Tests.Leaderboards.ApiModel;

namespace PitWallDataGatheringApi.Integration.Tests.Leaderboards
{
    public sealed class LeaderboardModelBuilder
    {
        private LeaderboardModel _leaderboardModel;

        public LeaderboardModelBuilder(string carName, string pilotName, string simerKey)
        {
            _leaderboardModel = new LeaderboardModel(carName, pilotName, simerKey);
        }

        public LeaderboardModelBuilder WithEntry(LeaderboardEntry entry)
        {
            _leaderboardModel.Entries.Add(entry);

            return this;
        }

        public LeaderboardModel Build()
        {
            return _leaderboardModel;
        }
    }
}
