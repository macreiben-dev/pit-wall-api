namespace PitWallDataGatheringApi.Repositories.Leaderboards;

public interface ILeaderboardInPitBoxRepository
{
    void Update(MetricDataWithSource<double?> metric);
}