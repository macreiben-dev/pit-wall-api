using PitWallDataGatheringApi.Repositories.Gauges;
using PitWallDataGatheringApi.Repositories.Gauges.Prom;

namespace PitWallDataGatheringApi.Repositories.Leaderboards
{
    public sealed class LeaderboardPitlaneRepository : ILeaderboardPitlaneRepository
    {
        private const string GaugeName = "pitwall_leaderaboard_is_pitlane";
        private readonly IGauge _gauge;

        public LeaderboardPitlaneRepository(IGaugeFactory gaugeFactory)
        {
            _gauge = gaugeFactory.Create(GaugeName, "Leaderboard 'is in pitlane flag'.", ConstantLabels.Labels);
        }
        
        public void Update(MetricData<double?> metric)
        {
            MetricDataToGauge.Execute(_gauge, metric);
        }
    }
}
