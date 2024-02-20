using MySql.Data.MySqlClient;
using PitWallDataGatheringApi.Models.Business.Leaderboards;
using PitWallDataGatheringApi.Repositories.Leaderboards;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using ZstdSharp.Unsafe;

namespace PitWallDataGatheringApi.Repositories.Gauges.Sql
{
    public sealed class LeaderboardSqlRepository : ILeaderboardRepository
    {
        private const string SerieNameCarNumberFormat = "pitwall_leaderboard_position{0}_carnumber";
        private const string SerieNameCarClassFormat = "pitwall_leaderboard_position{0}_carclass";

        private readonly ILeaderboardConnectionString _connectionString;

        public LeaderboardSqlRepository(ILeaderboardConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public void Update(ILeaderboardModel model)
        {
            var actualTick = DateTime.Now.Ticks;

            using (MySqlConnection connection = new MySqlConnection(_connectionString.ToString()))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
                {

                    var sql = @"INSERT INTO metric_leaderboard(pilot_name, car_name, data_tick, metric_name, metric_value) 
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
    }
}
