using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Prom;
using PitWallDataGatheringApi.Repositories.VehicleConsumptions;

namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public sealed class TrackEmperatureRepository : ITrackTemperatureRepository
    {
        private readonly IGauge _gauge;

        public TrackEmperatureRepository(IGaugeWrapperFactory _gaugeFactory)
        {
            _gauge = _gaugeFactory.Create(
                "pitwall_track_temperature_celsius",
                "Track temperature in celsius",
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
