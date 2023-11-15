using Prometheus;

namespace PitWallDataGatheringApi.Repositories.Prometheus
{
    public sealed class GaugeWrapper : IGauge
    {
        private readonly Gauge _gauge;
        private readonly HashSet<string> _labels;

        public GaugeWrapper(
            string serieName,
            string description,
            string[] labels)
        {
            if (serieName is null)
            {
                throw new ArgumentNullException(nameof(serieName));
            }

            if (serieName == string.Empty)
            {
                throw new ArgumentException($"Parameter '{nameof(serieName)}' cannot be empty.");
            }

            if (description is null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            if (description == string.Empty)
            {
                throw new ArgumentException($"Parameter '{nameof(description)}' cannot be empty.");
            }

            if (labels is null)
            {
                throw new ArgumentNullException(nameof(labels));
            }

            if (labels.Length == 0)
            {
                throw new ArgumentException($"Parameter '{nameof(labels)}' cannot be empty.");
            }

            var config = new GaugeConfiguration();

            config.LabelNames = labels;

            /**
             * Idea : create a factory to create gauge so repository can be full unit tested.
             * */
            _gauge = Metrics.CreateGauge(
                serieName,
                description,
                config);

            _labels = new HashSet<string>(labels);
        }

        public void Update(string label, double? data)
        {
            if (!_labels.Contains(label))
            {
                throw new LabelNotDeclaredException(label, data);
            }

            if (!data.HasValue)
            {
                return;
            }

            _gauge.WithLabels(label).Set(data.Value);
        }
    }
}
