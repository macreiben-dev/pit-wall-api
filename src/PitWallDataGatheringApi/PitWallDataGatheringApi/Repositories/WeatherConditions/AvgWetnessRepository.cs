using Prometheus;

namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public sealed class AvgWetnessRepository : IDocumentationLaptimeSerie, IAvgWetnessRepository
    {
        private const string LocalSerieName = "road_wetness_avg_percent";
        private readonly Gauge _gauge;
        readonly string[] _labels = new[] { "Pilot" };

        public string SerieName => LocalSerieName;

        public string[] Labels => _labels;

        public string Description => "Road wetness in percent.";

        public AvgWetnessRepository()
        {
            var config = new GaugeConfiguration();

            config.LabelNames = _labels;

            _gauge = Metrics.CreateGauge(
                SerieName,
                "Average road wetness in percent.",
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
