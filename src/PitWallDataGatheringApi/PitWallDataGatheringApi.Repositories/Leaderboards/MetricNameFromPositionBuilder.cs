using PitWallDataGatheringApi.Repositories.Leaderboards.CommandBuildes;

namespace PitWallDataGatheringApi.Repositories.Leaderboards
{
    public sealed class MetricNameFromPositionBuilder
    {
        private readonly DbMetricCommandBuilder commandbuilder;
        private readonly string metricNameFormat;
        private int _positionValue;

        public MetricNameFromPositionBuilder(DbMetricCommandBuilder commandbuilder, string metricNameFormat)
        {
            this.commandbuilder = commandbuilder;
            this.metricNameFormat = metricNameFormat;
        }

        public DbMetricCommandBuilder AndPositionValue(int positionValue)
        {
            _positionValue = positionValue;

            return commandbuilder.WithMetricFormatNameBuilder(this);
        }

        public string BuildMetricName()
        {
            return string.Format(metricNameFormat, _positionValue.ToString("D2"));
        }
    }
}
