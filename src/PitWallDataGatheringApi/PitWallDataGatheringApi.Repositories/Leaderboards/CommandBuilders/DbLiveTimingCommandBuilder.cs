using MySql.Data.MySqlClient;
using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Models.Business.Leaderboards;
using System.Data;

namespace PitWallDataGatheringApi.Repositories.Leaderboards.CommandBuildes
{
    public class DbLiveTimingCommandBuilder
    {
        private const string ParameterPilotName = "source_pilot_name";
        private const string ParameterCarName = "source_car_name";
        private const string ParameterDataTick = "data_tick";

        private const string ParameterMetricCarClass = "metric_car_class";
        private const string ParameterMetricCarNumber = "metric_car_number";
        private const string ParameterMetricPosition = "metric_position";

        private string? _carClass;
        private string? _carNumber;
        private int? _position;
        private long? _data_tick;

        private IDbCommand _command;

        public DbLiveTimingCommandBuilder(ISourceInfos model,
            long actualTick,
            int position,
            MySqlConnection connection,
            string sql)
        {

            _command = BuildCommand(model, position, actualTick, connection, sql);
        }

        private static IDbCommand BuildCommand(
            ISourceInfos model,
            int position,
            long actualTick,
            MySqlConnection connection,
            string sql)
        {
            var command = connection.CreateCommand();
            command.CommandText = sql;

            command.Parameters.Add(new MySqlParameter(ParameterPilotName, model.PilotName));
            command.Parameters.Add(new MySqlParameter(ParameterCarName, model.CarName));
            command.Parameters.Add(new MySqlParameter(ParameterDataTick, actualTick));
            command.Parameters.Add(new MySqlParameter(ParameterMetricPosition, position));

            return command;
        }
        public DbLiveTimingCommandBuilder WithCarClass(string? carClass)
        {
            _carClass = carClass;

            return this;
        }

        public DbLiveTimingCommandBuilder WithCarNumber(string? carNumber)
        {
            _carNumber = carNumber;

            return this;
        }

        public DbLiveTimingCommandBuilder WithPosition(int position)
        {
            _position = position;

            return this;
        }

        public IDbCommand AsCommand()
        {

            _command.Parameters.Add(
                new MySqlParameter(
                    ParameterMetricCarClass,
                    _carClass ?? "NA"));

            _command.Parameters.Add(
                new MySqlParameter(
                    ParameterMetricCarNumber,
                    _carNumber ?? "NA"));

            return _command;
        }

    }
}
