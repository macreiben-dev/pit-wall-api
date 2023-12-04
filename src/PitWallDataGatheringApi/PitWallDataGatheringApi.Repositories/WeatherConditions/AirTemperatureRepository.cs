using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Prom;
using PitWallDataGatheringApi.Repositories.VehicleConsumptions;

namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public sealed class AirTemperatureRepository :  IAirTemperatureRepository
    {
        private const string LocalSerieName = "pitwall_air_temperature_celsius";
        private readonly IGauge _gauge;

        private string Description => "Air temperature in celsius.";

        public AirTemperatureRepository(IGaugeWrapperFactory _gaugeFactory)
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
