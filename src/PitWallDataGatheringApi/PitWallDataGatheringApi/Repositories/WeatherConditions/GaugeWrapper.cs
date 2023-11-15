using Prometheus;
using System.Reflection.Emit;

namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public sealed class GaugeWrapper : IGaugeWrapper
    {
        private readonly Gauge _gauge;

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

            var config = new GaugeConfiguration();

            config.LabelNames = labels;

            _gauge = Metrics.CreateGauge(
                serieName,
                description,
                config);
        }

        public void Update(string label, double? data)
        {
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

    public class GaugeWrapperFactory : IGaugeWrapperFactory
    {

    }

    public interface IGaugeWrapperFactory
    {
    }
}
