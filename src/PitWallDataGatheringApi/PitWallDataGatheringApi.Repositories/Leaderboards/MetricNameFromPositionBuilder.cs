using PitWallDataGatheringApi.Repositories.Gauges.Sql;

namespace PitWallDataGatheringApi.Repositories.Leaderboards
{
    public sealed class MetricNameFromPositionBuilder
    {
        private readonly DbCommandBuilder commandbuilder;
        private readonly string metricNameFormat;
        private int _positionValue;

        public MetricNameFromPositionBuilder(DbCommandBuilder commandbuilder, string metricNameFormat)
        {
            this.commandbuilder = commandbuilder;
            this.metricNameFormat = metricNameFormat;
        }

        public DbCommandBuilder AndPositionValue(int positionValue)
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
