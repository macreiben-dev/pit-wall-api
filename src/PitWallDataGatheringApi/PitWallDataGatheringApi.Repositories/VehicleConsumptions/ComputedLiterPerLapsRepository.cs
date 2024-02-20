using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Gauges;
using PitWallDataGatheringApi.Repositories.Gauges.Prom;

namespace PitWallDataGatheringApi.Repositories.VehicleConsumptions
{
    public sealed class ComputedLiterPerLapsRepository : IComputedLiterPerLapsRepository
    {
        private const string LocalSerieName = "pitwall_computed_consumedfuelperlap_volume";
        private readonly IGauge _gauge;

        private string Description => "Computed - consumed fuel volume per lap.";


        public ComputedLiterPerLapsRepository(IGaugeFactory _gaugeFactory)
        {
            _gauge = _gaugeFactory.Create(
              LocalSerieName,
              Description,
              ConstantLabels.Labels);
        }

        public void Update(double? data, string pilotName, CarName carName)
        {
            _gauge.Update(new[] { pilotName, carName.ToString() }, data);
        }

        public void Update(MetricData<double?> metric)
        {
            MetricDataToGauge.Execute(_gauge, metric);
        }
    }
}
