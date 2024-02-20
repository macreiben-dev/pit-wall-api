using Dapper;
using MySql.Data.MySqlClient;
using PitWallDataGatheringApi.Repositories.Gauges.Sql;

namespace PitWallDataGatheringApi.Repositories.Integration.Tests.Gauges.Sql
{
    public class LeaderboardSqlRepositoryTest
    {
        private const int EntryPosition = 13;

        private const string SqlConnectionString = "Server=localhost;Port=3306;Database=pitwall_leaderboard;User=root;Password=password;ConvertZeroDateTime=True;";

        private LeaderboardSqlRepository GetTarget()
        {
            return new LeaderboardSqlRepository(new FakeConnectionString(SqlConnectionString));
        }

        public LeaderboardSqlRepositoryTest() {

            using (var connection = new MySqlConnection(SqlConnectionString))
            {
                // Create a query that retrieves all books with an author name of "John Smith"    
                var sql = "DELETE FROM metric_leaderboard  WHERE pilot_name = @pilot_name AND car_name = @car_name";

                // Use the Query method to execute the query and return a list of objects    
                _ = connection.Query<ActualLeaderboardRow>(sql, new
                {
                    pilot_name = "PilotFromIntegrationTest01",
                    car_name = "CarFromIntegrationTest01"
                });
            }
        }

        [Fact]
        public void GIVEN_entry_AND_pilotName_AND_carName_THEN_update_gauge()
        {
            // ARRANGE
            var model = new FakeLeaderboardModel()
            {
                CarName = new Models.CarName("CarFromIntegrationTest01"),
                PilotName = new Models.PilotName("PilotFromIntegrationTest01"),
            };

            var entry = new FakeBusinessEntry()
            {

                CarClass = "GTE",
                CarNumber = "53",
                LastPitLap = 12,
                Position = EntryPosition
            };

            model.AddEntry(entry);

            // ACT
            var target = GetTarget();

            target.Update(model);

            // ASSERT
            var actual = GetFrom(model);
        }


        private IEnumerable<ActualLeaderboardRow> GetFrom(FakeLeaderboardModel original)
        {
            using (var connection = new MySqlConnection(SqlConnectionString))
            {
                // Create a query that retrieves all books with an author name of "John Smith"    
                var sql = "SELECT * FROM metric_leaderboard WHERE pilot_name = @pilot_name AND car_name = @car_name";

                // Use the Query method to execute the query and return a list of objects    
                var entries = connection.Query<ActualLeaderboardRow>(sql, new { 
                    pilot_name = "PilotFromIntegrationTest01",
                    car_name = "CarFromIntegrationTest01"
                }).ToList();

                return entries;
            }
        }
    }
}
