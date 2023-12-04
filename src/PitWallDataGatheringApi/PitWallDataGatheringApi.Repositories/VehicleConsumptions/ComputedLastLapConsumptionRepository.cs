using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Prom;

namespace PitWallDataGatheringApi.Repositories.VehicleConsumptions
{
    public sealed class ComputedLastLapConsumptionRepository : IComputedLastLapConsumptionRepository
    {
        private const string LocalSerieName = "pitwall_computed_lastlapconsumption_volume";
        private readonly IGauge _gauge;

        private string Description => "Computed - last lap consumption.";


        public ComputedLastLapConsumptionRepository(IGaugeWrapperFactory _gaugeFactory)
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
