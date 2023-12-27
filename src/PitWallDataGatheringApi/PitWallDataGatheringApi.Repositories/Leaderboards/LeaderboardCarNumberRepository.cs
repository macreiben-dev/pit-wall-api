using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Models.Business.Leaderboards;
using PitWallDataGatheringApi.Repositories.Prom;
using PitWallDataGatheringApi.Repositories.VehicleConsumptions;

namespace PitWallDataGatheringApi.Repositories.Leaderboards
{
    public sealed class LeaderboardCarNumberRepository : ILeaderboardCarNumberRepository
    {
        private const string LocalSerieNameFormat = "pitwall_leaderboard_position{0}_carnumber";
        private const string LocalDescrptionFormat = "Entry information for position {0}.";
        private IGaugeFactory _gaugeFactory;

        public LeaderboardCarNumberRepository(IGaugeFactory gaugeFactory)
        {
            _gaugeFactory = gaugeFactory;
        }

        public void Update(ILeaderboardEntry entry, PilotName pilotName, CarName carName)
        {
            var gauge = _gaugeFactory.CreateLeaderboardGauge(
                     LocalSerieNameFormat,
                     LocalDescrptionFormat,
                     entry.Position,
                     ConstantLabels.Labels);

            Update(new MetricData<double?>(entry.CarNumber, pilotName, carName), gauge);
        }

        public void Update(MetricData<double?> metric, IGauge gauge)
        {
            MetricDataToGauge.Execute(gauge, metric);
        }
    }
}
