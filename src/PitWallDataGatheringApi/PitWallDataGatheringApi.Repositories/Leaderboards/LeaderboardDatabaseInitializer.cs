using MySql.Data.MySqlClient;
using PitWallDataGatheringApi.Repositories.Gauges.Sql;
using Dapper;
using Microsoft.Extensions.Logging;

namespace PitWallDataGatheringApi.Repositories.Leaderboards
{
    public class LeaderboardDatabaseInitializer : ILeaderboardDatabaseInitializer
    {
        private ILeaderboardConnectionString _connectionString;
        private readonly ILogger<LeaderboardDatabaseInitializer> _logger;

        public LeaderboardDatabaseInitializer(ILeaderboardConnectionString connectionString, ILogger<LeaderboardDatabaseInitializer> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public void Init()
        {
            _logger.LogInformation("Creating Leaderboard Database ...");

            using var connection = new MySqlConnection(_connectionString.ToString());

            {
                var sql = @"CREATE DATABASE IF NOT EXISTS `pitwall_leaderboard`;";

                Task.WaitAll(connection.ExecuteAsync(sql));
            }

            _logger.LogInformation("Creating Leaderboard Database ... DONE");

            // ===

            _logger.LogInformation("Creating Leaderboard Tables ...");
            {
                var sql = @"
                CREATE TABLE IF NOT EXISTS pitwall_leaderboard.metric_leaderboard(
                    pilot_name VARCHAR(50),
                    car_name VARCHAR(50),
                    data_tick BIGINT,
                    metric_name VARCHAR(100),
                    metric_value VARCHAR(100),
                    PRIMARY KEY(data_tick, pilot_name, car_name, metric_name)
                );";

                Task.WaitAll(connection.ExecuteAsync(sql));
            }
            _logger.LogInformation("Creating Leaderboard Tables ... DONE");
        }
    }
}
