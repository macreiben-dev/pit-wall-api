using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Gauges;
using PitWallDataGatheringApi.Repositories.Gauges.Prom;

namespace PitWallDataGatheringApi.Repositories.VehicleConsumptions
{
    public sealed class ComputedRemainingLapsRepository : IComputedRemainingLapsRepository
    {
        private const string LocalSerieName = "pitwall_computed_remaininglap_volume";
        private readonly IGauge _gauge;

        private string Description => "Computed - remaining lap given current fuel volume.";


        public ComputedRemainingLapsRepository(IGaugeFactory _gaugeFactory)
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
