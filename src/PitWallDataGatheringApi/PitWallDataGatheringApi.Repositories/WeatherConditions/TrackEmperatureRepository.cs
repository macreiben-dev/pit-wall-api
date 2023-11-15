using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Prometheus;

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
                new[] { "Pilot", "All", "Car" });
        }

        public void Update(double? dataValue, string pilotName, CarName carName)
        {
            _gauge.Update(new[] { pilotName, "All", carName.ToString() }, dataValue);
        }
    }
}
