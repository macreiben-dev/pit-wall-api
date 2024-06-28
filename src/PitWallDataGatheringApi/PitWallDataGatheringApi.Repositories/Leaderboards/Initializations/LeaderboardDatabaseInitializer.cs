using MySql.Data.MySqlClient;
using PitWallDataGatheringApi.Repositories.Gauges.Sql;
using Dapper;
using Microsoft.Extensions.Logging;

namespace PitWallDataGatheringApi.Repositories.Leaderboards.Initializations
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

            CreateDatabaseIfNotExists(connection);

            // ===

            CreateLeaderBoardTableIfNotExists(connection);

            {
                try
                {
                    var sql = "ALTER TABLE pitwall_leaderboard.metric_leaderboard_livetiming " +
                              "DROP INDEX idx_metric_leaderboard_data_query;";

                    Task.WaitAll(connection.ExecuteAsync(sql));
                }
                catch
                {
                    _logger.LogInformation("Unable to drop index. Index may not exists.");
                }
            }
            
            {
                var sql = "ALTER TABLE pitwall_leaderboard.metric_leaderboard_livetiming " +
                          "ADD INDEX idx_metric_leaderboard_data_query (data_tick, source_pilot_name, source_car_name);";

                Task.WaitAll(connection.ExecuteAsync(sql));
                
                _logger.LogInformation("Index idx_metric_leaderboard_data_query create.");
            }
            _logger.LogInformation("Creating Leaderboard Tables ... DONE");
        }

        private void CreateLeaderBoardTableIfNotExists(MySqlConnection connection)
        {
            _logger.LogInformation("Creating Leaderboard Tables ...");

            {
                var sql = @"
                CREATE TABLE IF NOT EXISTS pitwall_leaderboard.metric_leaderboard_livetiming(
                    source_pilot_name VARCHAR(50) NOT NULL,
                    source_car_name VARCHAR(50) NOT NULL,
                    data_tick BIGINT NOT NULL,
                    metric_car_class VARCHAR(50) NULL,
                    metric_car_number VARCHAR(50) NULL,
                    metric_position VARCHAR(2) NULL
                );";

                Task.WaitAll(connection.ExecuteAsync(sql));
            }
            
            _logger.LogInformation("Creating Leaderboard Tables ... DONE!");
        }

        private void CreateDatabaseIfNotExists(MySqlConnection connection)
        {
            {
                var sql = @"CREATE DATABASE IF NOT EXISTS `pitwall_leaderboard`;";

                Task.WaitAll(connection.ExecuteAsync(sql));
            }

            _logger.LogInformation("Creating Leaderboard Database ... DONE");
        }
    }
}
