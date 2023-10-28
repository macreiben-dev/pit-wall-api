using Prometheus;

namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public sealed class AirTemperatureRepository : IDocumentationAirTemperatureSerie, IAirTemperatureRepository
    {
        private const string LocalSerieName = "air_temperature_celsius";
        private readonly Gauge _gauge;
        readonly string[] _labels = new[] { "Pilot" };

        public string SerieName => LocalSerieName;

        public string[] Labels => _labels;

        public string Description => "Road wetness in percent.";

        public AirTemperatureRepository()
        {
            var config = new GaugeConfiguration();

            config.LabelNames = _labels;

            _gauge = Metrics.CreateGauge(
                SerieName,
                "Air temperature in celsius.",
                config);
        }

        public void Update(double? laptime, string pilotName)
        {
            UpdateGauge(
               laptime,
               pilotName,
               _gauge);

            UpdateGauge(
                laptime,
                "All",
                _gauge);
        }

        private void UpdateGauge(double? data, string gaugeLabel, Gauge gauge)
        {
            if (!data.HasValue)
            {
                return;
            }

            gauge.WithLabels(gaugeLabel).Set(data.Value);
        }
    }
}
