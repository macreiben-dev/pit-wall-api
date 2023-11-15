using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Prometheus;

namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public sealed class AirTemperatureRepository : IDocumentationAirTemperatureSerie, IAirTemperatureRepository
    {
        private const string LocalSerieName = "pitwall_air_temperature_celsius";
        private readonly IGauge _gauge;
        readonly string[] _labels = new[] { "Pilot", "All", "Car" };

        public string SerieName => LocalSerieName;

        public string[] Labels => _labels;

        public string Description => "Road wetness in percent.";

        public AirTemperatureRepository(IGaugeWrapperFactory _gaugeFactory)
        {
            _gauge = _gaugeFactory.Create(
              LocalSerieName,
              Description,
              ConstantLabels.Labels);
        }

        public void Update(double? data, string pilotName, CarName carName)
        {
            _gauge.Update(new[] { pilotName, "All", carName.ToString() }, data);
        }
    }
}
