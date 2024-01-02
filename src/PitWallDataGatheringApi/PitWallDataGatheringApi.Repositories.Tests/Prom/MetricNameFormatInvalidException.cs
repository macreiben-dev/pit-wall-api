namespace PitWallDataGatheringApi.Repositories.Tests.Prom
{
    public sealed class MetricNameFormatInvalidException : Exception
    {
        public MetricNameFormatInvalidException(string metricFormat) :
            base($"Metric name format is invalid. Please provide a placeholder for position. Given metric was [{metricFormat}]. Example : metric_{{0}}_someData." )
        {
            MetricName = metricFormat;
        }

        public string MetricName { get; }
    }
}