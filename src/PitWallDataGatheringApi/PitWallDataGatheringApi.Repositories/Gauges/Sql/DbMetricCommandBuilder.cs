using MySql.Data.MySqlClient;
using PitWallDataGatheringApi.Models.Business.Leaderboards;
using PitWallDataGatheringApi.Repositories.Leaderboards;
using System.Data;

namespace PitWallDataGatheringApi.Repositories.Gauges.Sql
{
    public class DbMetricCommandBuilder
    {
        private const string ParameterPilotName = "pilot_name";
        private const string ParameterCarName = "car_name";
        private const string ParameterDataTick = "data_tick";
        private const string ParameterMetricName = "metric_name";
        private const string ParameterMetricValue = "metric_value";

        private IDbCommand _command;
        private string _format;
        private int _positionValue;
        private MetricNameFromPositionBuilder _metricNameFromPosition;
        private string _metricValue;

        public DbMetricCommandBuilder(
            ILeaderboardModel model,
            long actualTick,
            MySqlConnection connection,
            string sql)
        {
            _command = BuildCommand(model, actualTick, connection, sql);
        }

        public MetricNameFromPositionBuilder WithMetricNameFormat(string metricNameFormat)
        {
            return new MetricNameFromPositionBuilder(this, metricNameFormat);
        }

        public DbMetricCommandBuilder WithMetricFormatNameBuilder(MetricNameFromPositionBuilder metricNameFromPosition)
        {
            _metricNameFromPosition = metricNameFromPosition;

            return this;
        }

        public DbMetricCommandBuilder WithMetricValue(string metricValue)
        {
            _metricValue = metricValue;

            return this;
        }

        public IDbCommand AsCommand()
        {
            _command.Parameters.Add(
                new MySqlParameter(
                    ParameterMetricName,
                    _metricNameFromPosition.BuildMetricName()));

            _command.Parameters.Add(
                new MySqlParameter(
                    ParameterMetricValue,
                    _metricValue));

            return _command;
        }

        private static IDbCommand BuildCommand(
            ILeaderboardModel model,
            long actualTick,
            MySqlConnection connection,
            string sql)
        {
            var command = connection.CreateCommand();
            command.CommandText = sql;

            command.Parameters.Add(new MySqlParameter(ParameterPilotName, model.PilotName));
            command.Parameters.Add(new MySqlParameter(ParameterCarName, model.CarName));
            command.Parameters.Add(new MySqlParameter(ParameterDataTick, actualTick));

            return command;
        }

    }
}
