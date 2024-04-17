using MySql.Data.MySqlClient;
using PitWallDataGatheringApi.Models.Business.Leaderboards;
using PitWallDataGatheringApi.Repositories.Gauges.Sql;
using PitWallDataGatheringApi.Repositories.Leaderboards.CommandBuildes;
using System.Data;
using PitWallDataGatheringApi.Repositories.Leaderboards.CommandBuilders;

namespace PitWallDataGatheringApi.Repositories.Leaderboards.Updates
{
    public sealed class LeaderboardLivetimingSqlRepository(ILeaderboardConnectionString connectionString)
        : ILeaderboardLivetimingSqlRepository
    {
        private const string InsertStatement = @"INSERT INTO pitwall_leaderboard.metric_leaderboard_livetiming(
                        source_pilot_name, 
                        source_car_name, 
                        data_tick, 
                        metric_car_number,
                        metric_car_class,
                        metric_position) 
                    VALUES(
                        @source_pilot_name, 
                        @source_car_name, 
                        @data_tick, 
                        @metric_car_number,  
                        @metric_car_class, 
                        @metric_position)";

        public void Update(ILeaderboardModel model)
        {
            var actualTick = DateTime.Now.Ticks;

            using MySqlConnection connection = new MySqlConnection(connectionString.ToString());
            connection.Open();

            using var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            foreach (var entry in model)
            {
                var commandBuilder = new DbLiveTimingCommandBuilder(
                        model,
                        actualTick,
                        entry.Position,
                        connection,
                        InsertStatement);

                commandBuilder
                    .WithCarNumber(entry.CarNumber)
                    .WithCarClass(entry.CarClass);

                commandBuilder
                        .AsCommand()
                        .ExecuteNonQuery();
            }

            transaction.Commit();
        }

        public void Clear()
        {
            using MySqlConnection connection = new MySqlConnection(connectionString.ToString());
            connection.Open();

            var command = connection.CreateCommand();
            
            command.CommandText = "DELETE FROM pitwall_leaderboard.metric_leaderboard_livetiming";

            command.ExecuteNonQuery();
        }
    }
}
