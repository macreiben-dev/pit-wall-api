using Microsoft.Extensions.Logging;
using Prometheus;

namespace PitWallDataGatheringApi.Repositories.Prom
{
    public sealed class GaugeWrapper : IGauge, ISerieDocumentation
    {
        private readonly Gauge _gauge;
        private readonly HashSet<string> _labels;
        private readonly string _labelsContactenated;
        private ILogger<GaugeWrapper> _logger;

        public GaugeWrapper(
            string serieName,
            string description,
            IEnumerable<string> labels, 
            ILogger<GaugeWrapper> logger)
        {
            _logger = logger;

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

            if (labels.Count() == 0)
            {
                throw new ArgumentException($"Parameter '{nameof(labels)}' cannot be empty.");
            }

            var config = new GaugeConfiguration();

            config.LabelNames = labels.ToArray();

            /**
             * Idea : create a factory to create gauge so repository can be full unit tested.
             * */
            _gauge = Metrics.CreateGauge(
                serieName,
                description,
                config);

            _labels = new HashSet<string>(labels);

            _labelsContactenated = string.Join(",", _labels);

            Description = description;

            SerieName = serieName;
        }

        public string Description { get; }
        public string SerieName { get; }

        public IEnumerable<string> Labels => _labels;

        public void Update(IEnumerable<string> labels, double? dataValue)
        {
            if (labels.Count() != _labels.Count)
            {
                throw new LabelCountMustMatchDeclaredLabelsException(_labels, labels);
            }

            if (!dataValue.HasValue)
            {
                return;
            }

            _gauge.WithLabels(labels.ToArray()).Set(dataValue.Value);

            _logger.LogDebug($"Update [{SerieName}] - [{_labelsContactenated}] - [{dataValue}]");
        }

        public void Update(string label, double? data)
        {
            if (_labels.Count != 1)
            {
                throw new LabelCountMustMatchDeclaredLabelsException(_labels, new[] { label });
            }

            if (!data.HasValue)
            {
                return;
            }

            _gauge.WithLabels(label).Set(data.Value);
        }
    }
}
