using Newtonsoft.Json.Linq;
using Prometheus;

namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public sealed class GaugeWrapper : IGaugeWrapper
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

            if(serieName == string.Empty)
            {
                throw new ArgumentException($"Parameter '{nameof(serieName)}' cannot be empty.");
            }

            if(description is null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            if (description == string.Empty)
            {
                throw new ArgumentException($"Parameter '{nameof(description)}' cannot be empty.");
            }

            if(labels is null)
            {
                throw new ArgumentNullException(nameof(labels));
            }

            if (labels.Length == 0)
            {
                throw new ArgumentException($"Parameter '{nameof(labels)}' cannot be empty.");
            }

            var config = new GaugeConfiguration();

            config.LabelNames = labels;

            _gauge = Metrics.CreateGauge(
                serieName,
                description,
                config);

            _labels = new HashSet<string>(labels);
        }

        public void Update(string label, double? data)
        {
            if(!_labels.Contains(label))
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

    public interface IGaugeWrapper
    {
        void Update(string label, double? value);
    }

    public sealed class GaugeWrapperFactory : IGaugeWrapperFactory
    {

    }

    public interface IGaugeWrapperFactory
    {
        
    }

    public sealed class LabelNotDeclaredException : Exception
    {
        public LabelNotDeclaredException(string label, double? value)
        {
            LabelName = label;
            Value = value;
        }

        public string LabelName { get; }
        public double? Value { get; }
    }
}
