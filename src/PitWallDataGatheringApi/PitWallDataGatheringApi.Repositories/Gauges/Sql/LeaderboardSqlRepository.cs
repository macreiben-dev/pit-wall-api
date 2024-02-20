using MySql.Data.MySqlClient;
using PitWallDataGatheringApi.Models.Business.Leaderboards;
using PitWallDataGatheringApi.Repositories.Leaderboards;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace PitWallDataGatheringApi.Repositories.Gauges.Sql
{
    public sealed class LeaderboardSqlRepository : ILeaderboardRepository
    {
        private const string SerieNameCarNumberFormat = "pitwall_leaderboard_position{0}_carnumber";
        private const string SerieNameCarClassFormat = "pitwall_leaderboard_position{0}_carclass";

        private const string ParameterPilotName = "pilot_name";
        private const string ParameterCarName = "car_name";
        private const string ParameterDataTick = "data_tick";
        private const string ParameterMetricName = "metric_name";
        private const string ParameterMetricValue = "metric_value";

        private const string SqlConnectionString = "Server=sqldatabase;Database=YourDatabaseName;User=root;Password=some_password;ConvertZeroDateTime=True;";

        public void Update(ILeaderboardModel model)
        {
            /**
             * We might have an issue here since labels will be Pilot/Car exclusively in this order.
             * 
             * We should use a structure. That might end in a liskov breach in the end.
             * */
            var actualTick = DateTime.Now.Ticks;

            using (MySqlConnection connection = new MySqlConnection(SqlConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
                {

                    var sql = @"INSERT INTO leaderboard_data(pilot_name, car_name, data_tick, metric_name, metric_value) 
                    VALUES(@pilot_name, @car_name, @data_tick, @metric_name, @metric_value)";

                    foreach (var entry in model)
                    {
                        {
                            var commandBuilder = new DbCommandBuilder(model, actualTick, connection, sql);

                            commandBuilder.WithMetricNameFormat(SerieNameCarNumberFormat)
                                .AndPositionValue(entry.Position)
                                .WithMetricValue(entry.CarNumber);

                            commandBuilder
                                .AsCommand()
                                .ExecuteNonQuery();
                        }

                        {
                            var commandBuilder = new DbCommandBuilder(model, actualTick, connection, sql);

                            commandBuilder.WithMetricNameFormat(SerieNameCarClassFormat)
                                .AndPositionValue(entry.Position)
                                .WithMetricValue(entry.CarClass);

                            commandBuilder
                                .AsCommand()
                                .ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
            }
        }

        private static IDbCommand BuildCommand(ILeaderboardModel model, long actualTick, MySqlConnection connection, string sql, ILeaderboardEntry entry)
        {
            var command = connection.CreateCommand();
            command.CommandText = sql;

            command.Parameters.Add(new MySqlParameter(ParameterPilotName, model.PilotName));
            command.Parameters.Add(new MySqlParameter(ParameterCarName, model.CarName));
            command.Parameters.Add(new MySqlParameter(ParameterDataTick, actualTick));

            return command;
        }
    }

    public class DbCommandBuilder
    {
        private const string SerieNameCarNumberFormat = "pitwall_leaderboard_position{0}_carnumber";
        private const string SerieNameCarClassFormat = "pitwall_leaderboard_position{0}_carclass";

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

        public DbCommandBuilder(
            ILeaderboardModel model,
            long actualTick,
            MySqlConnection connection,
            string sql)
        {
            _command = BuildCommand(model, actualTick, connection, sql);
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
            command.Parameters.Add(new MySqlParameter(ParameterCarName, model.CarName + "  "));gdfg
            command.Parameters.Add(new MySqlParameter(ParameterDataTick, actualTick));

            return command;
        }

        public MetricNameFromPositionBuilder WithMetricNameFormat(string metricNameFormat)
        {
            return new MetricNameFromPositionBuilder(this, metricNameFormat);
        }

        public DbCommandBuilder WithMetricFormatNameBuilder(MetricNameFromPositionBuilder metricNameFromPosition)
        {
            _metricNameFromPosition = metricNameFromPosition;

            return this;
        }

        public DbCommandBuilder WithMetricValue(string metricValue)
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
    }

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
            return string.Format(metricNameFormat, _positionValue.ToString("D2");
        }
    }
}
