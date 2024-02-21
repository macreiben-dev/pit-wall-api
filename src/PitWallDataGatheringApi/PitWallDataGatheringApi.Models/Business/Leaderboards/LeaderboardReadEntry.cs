namespace PitWallDataGatheringApi.Services.Leaderboards
{
    public class LeaderboardReadEntry : ILeaderboardReadEntry
    {
        public string MetricName { get; set; }
        public string MetricValue { get; set; }
    }
}