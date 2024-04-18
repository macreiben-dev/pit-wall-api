namespace PitWallDataGatheringApi.Services.Leaderboards
{
    public interface ILeaderboardReadEntry
    {
        public string MetricName { get; }
        public string MetricValue { get; }
    }
}