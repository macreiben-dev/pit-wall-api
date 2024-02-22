using MySql.Data.MySqlClient;
using PitWallDataGatheringApi.Repositories.Gauges.Sql;
using PitWallDataGatheringApi.Services.Leaderboards;
using Dapper;

namespace PitWallDataGatheringApi.Repositories.Leaderboards
{
    public sealed class ReadLeaderboardRepository : IReadLeaderboardRepository
    {
        private ILeaderboardConnectionString _connectionString;

        public ReadLeaderboardRepository(ILeaderboardConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<ILeaderboardReadEntry> Get(string pilotName, string carName)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString.ToString()))
            {
                connection.Open();

                connection.ChangeDatabase("pitwall_leaderboard");

                var query = @"
                    SELECT 
                        metric_name MetricName, 
                        metric_value MetricValue
                    FROM metric_leaderboard 
                    WHERE 1 = 1
                    AND data_tick = (SELECT MAX(data_tick) FROM metric_leaderboard)
                    AND pilot_name = @pilotname
                    AND car_name = @carName";

                var result = connection.Query<LeaderboardReadEntry>(query, new { pilotName, carName });

                return result;
            }
        }
    }
}
