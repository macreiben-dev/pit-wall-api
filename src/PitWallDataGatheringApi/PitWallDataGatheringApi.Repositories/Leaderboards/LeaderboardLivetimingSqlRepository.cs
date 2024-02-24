using MySql.Data.MySqlClient;
using PitWallDataGatheringApi.Models.Business.Leaderboards;
using PitWallDataGatheringApi.Repositories.Gauges.Sql;
using System.Data;

namespace PitWallDataGatheringApi.Repositories.Leaderboards
{
    public sealed class LeaderboardLivetimingSqlRepository : ILeaderboardLivetimingSqlRepository
    {
        private readonly ILeaderboardConnectionString _connectionString;

        public LeaderboardLivetimingSqlRepository(ILeaderboardConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public void Update(ILeaderboardModel model)
        {
            var actualTick = DateTime.Now.Ticks;

            using (MySqlConnection connection = new MySqlConnection(_connectionString.ToString()))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted))
                {

                    var sql = @"INSERT INTO pitwall_leaderboard.metric_leaderboard_livetiming(
                        source_pilot_name, 
                        source_car_name, 
                        data_tick, 
                        metric_car_number,
                        metric_car_class,
                        metric_position) 
                    VALUES(@source_pilot_name, @source_car_name, @data_tick, @metric_car_number,  @metric_car_class, @metric_position)";

                    foreach (var entry in model)
                    {
                        {
                            var commandBuilder = new DbLiveTimingCommandBuilder(model, actualTick, entry.Position, connection, sql);

                            commandBuilder
                                .WithCarNumber(entry.CarNumber)
                                .WithCarClass(entry.CarClass);

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
