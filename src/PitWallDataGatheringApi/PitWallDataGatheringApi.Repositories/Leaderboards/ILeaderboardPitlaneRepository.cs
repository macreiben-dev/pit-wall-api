namespace PitWallDataGatheringApi.Repositories.Leaderboards;

public interface ILeaderboardPitlaneRepository
{
    void Update(MetricDataWithSource<double?> metric);
}