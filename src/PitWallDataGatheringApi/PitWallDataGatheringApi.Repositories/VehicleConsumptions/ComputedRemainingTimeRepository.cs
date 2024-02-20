using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Gauges;
using PitWallDataGatheringApi.Repositories.Gauges.Prom;

namespace PitWallDataGatheringApi.Repositories.VehicleConsumptions
{
    public sealed class ComputedRemainingTimeRepository : IComputedRemainingTimeRepository
    {
        private const string LocalSerieName = "pitwall_computed_remainingtimeonfuel_seconds";
        private readonly IGauge _gauge;

        private string Description => "Computed - remaining duration until fuel runs out.";

        public ComputedRemainingTimeRepository(IGaugeFactory _gaugeFactory)
        {
            _gauge = _gaugeFactory.Create(
              LocalSerieName,
              Description,
              ConstantLabels.Labels);
        }

        public void Update(double? data, string pilotName, CarName carName)
        {
            Update(new MetricData<double?>(data, new PilotName(pilotName), carName));
        }

        public void Update(MetricData<double?> metric)
        {
            MetricDataToGauge.Execute(_gauge, metric);
        }
    }
}
