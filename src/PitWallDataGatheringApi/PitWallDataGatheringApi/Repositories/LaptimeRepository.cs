using Prometheus;

namespace PitWallDataGatheringApi.Repositories
{
    public sealed class LaptimeRepository : ILaptimeRepository
    {
        private readonly Gauge _gaugeLapTimes;
        readonly string[] pilotLabels = new[] { "Pilot" };

        public LaptimeRepository()
        {
            var config = new GaugeConfiguration();

            config.LabelNames = pilotLabels;

            _gaugeLapTimes = Metrics.CreateGauge(
                "pitwall_laptimes_seconds",
                "Car laptimes in seconds.",
                config);
        }

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
