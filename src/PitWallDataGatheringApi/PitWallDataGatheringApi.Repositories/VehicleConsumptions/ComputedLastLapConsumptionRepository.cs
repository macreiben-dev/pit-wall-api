using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Gauges;
using PitWallDataGatheringApi.Repositories.Gauges.Prom;

namespace PitWallDataGatheringApi.Repositories.VehicleConsumptions
{
    public sealed class ComputedLastLapConsumptionRepository : IComputedLastLapConsumptionRepository
    {
        private const string LocalSerieName = "pitwall_computed_lastlapconsumption_volume";
        private readonly IGauge _gauge;

        private string Description => "Computed - last lap consumption.";
        
        public ComputedLastLapConsumptionRepository(IGaugeFactory gaugeFactory)
        {
            _gauge = gaugeFactory.Create(
              LocalSerieName,
              Description,
              ConstantLabels.Labels);
        }

        public void Update(MetricData<double?> metric)
        {
            MetricDataToGauge.Execute(_gauge, metric);
        }
    }
}
