using PitWallDataGatheringApi.Repositories.Gauges;
using PitWallDataGatheringApi.Repositories.Gauges.Prom;
using PitWallDataGatheringApi.Repositories.VehicleConsumptions;

namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public sealed class TrackEmperatureRepository : ITrackTemperatureRepository
    {
        private readonly IGauge _gauge;

        public TrackEmperatureRepository(IGaugeFactory _gaugeFactory)
        {
            _gauge = _gaugeFactory.Create(
                "pitwall_track_temperature_celsius",
                "Track temperature in celsius",
                ConstantLabels.Labels);
        }

        public void Update(MetricData<double?> metric)
        {
            MetricDataToGauge.Execute(_gauge, metric);
        }
    }
}
