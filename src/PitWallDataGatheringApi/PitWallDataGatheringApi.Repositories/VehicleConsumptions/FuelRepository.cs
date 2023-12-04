using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Prom;

namespace PitWallDataGatheringApi.Repositories.VehicleConsumptions
{
    public sealed class FuelRepository : IFuelRepository
    {
        private const string LocalSerieName = "pitwall_fuel_volume";
        private readonly IGauge _gauge;

        private string Description => "Fuel volume in tank.";

        public FuelRepository(IGaugeWrapperFactory _gaugeFactory)
        {
            _gauge = _gaugeFactory.Create(
              LocalSerieName,
              Description,
              ConstantLabels.Labels);
        }

        public void Update(double? data, string pilotName, CarName carName)
        {
            Update(new MetricData<double?>(data, carName, new PilotName(pilotName)));
        }

        public void Update(MetricData<double?> metric)
        {
            MetricDataToGauge.Execute(_gauge, metric);
        }
    }
}
