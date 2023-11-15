using Prometheus;

namespace PitWallDataGatheringApi.Repositories
{
    public sealed class LaptimeRepository : ILaptimeRepository, IDocumentationLaptimeSerie
    {
        private const string LocalSerieName = "pitwall_laptimes_seconds";
        private readonly Gauge _gaugeLapTimes;
        readonly string[] pilotLabels = new[] { "Pilot" };

        public LaptimeRepository()
        {
            var config = new GaugeConfiguration();

            config.LabelNames = pilotLabels;

            _gaugeLapTimes = Metrics.CreateGauge(
                SerieName,
                "Car laptimes in seconds.",
                config);
        }

        public string SerieName => LocalSerieName;

        public string[] Labels => pilotLabels;

        public string Description => "Last laptime retrieved from source. If laptime is invalid, then it's 0.";

        public void Update(double? laptime, string pilotName)
        {
            UpdateGauge(
               laptime,
               pilotName,
               _gaugeLapTimes);

            UpdateGauge(
                laptime,
                "All",
                _gaugeLapTimes);
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
