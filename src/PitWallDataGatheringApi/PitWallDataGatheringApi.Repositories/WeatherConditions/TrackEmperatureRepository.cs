using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Prom;

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

        public void Update(double? dataValue, string pilotName, CarName carName)
        {
            _gauge.Update(new[] { pilotName, carName.ToString() }, dataValue);
        }
    }
}
