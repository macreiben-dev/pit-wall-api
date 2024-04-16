namespace PitWallDataGatheringApi.Integration.Tests.Leaderboards.ApiModel
{
    public sealed class LeaderboardModel
    {
        public LeaderboardModel(string carName, string pilotName, string simerKey)
        {
            PilotName = pilotName;

            CarName = carName;

            SimerKey = simerKey;

            Entries = new List<LeaderboardEntry>();
        }

        public string? PilotName { get; set; }

        public string? CarName { get; set; }

        public string SimerKey { get; set; }

        public IList<LeaderboardEntry> Entries { get; set; }
    }
}
