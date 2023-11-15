using Prometheus;

namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public sealed class AirTemperatureRepository : IDocumentationAirTemperatureSerie, IAirTemperatureRepository
    {
        private const string LocalSerieName = "pitwall_air_temperature_celsius";
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

        public void Update(double? data, string pilotName)
        {
            UpdateGauge(
               data,
               pilotName,
               _gauge);

            UpdateGauge(
                data,
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
