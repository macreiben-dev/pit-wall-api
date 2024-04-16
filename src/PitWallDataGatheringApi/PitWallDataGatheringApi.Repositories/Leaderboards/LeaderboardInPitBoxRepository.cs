using PitWallDataGatheringApi.Repositories.Gauges;
using PitWallDataGatheringApi.Repositories.Gauges.Prom;

namespace PitWallDataGatheringApi.Repositories.Leaderboards;

public class LeaderboardInPitBoxRepository : ILeaderboardInPitBoxRepository
{
    private const string GaugeName = "pitwall_leaderboard_isinpitbox";
    private readonly IGauge _gauge;

    public LeaderboardInPitBoxRepository(IGaugeFactory gaugeFactory)
    {
        _gauge = gaugeFactory.Create(GaugeName, "Leaderboard 'is in pitbox flag'.", ConstantLabels.Labels);
    }
    public void Update(MetricData<double?> metric)
    {
        MetricDataToGauge.Execute(_gauge, metric);
    }
}