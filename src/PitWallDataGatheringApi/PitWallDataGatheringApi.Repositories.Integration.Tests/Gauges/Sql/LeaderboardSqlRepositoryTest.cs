using Dapper;
using MySql.Data.MySqlClient;
using NFluent;
using PitWallDataGatheringApi.Repositories.Leaderboards;

namespace PitWallDataGatheringApi.Repositories.Integration.Tests.Gauges.Sql
{
    public class LeaderboardSqlRepositoryTest
    {
        private const int EntryPosition = 13;

        private const string SqlConnectionString = "Server=localhost;Port=3306;Database=pitwall_leaderboard;User=root;Password=password;ConvertZeroDateTime=True;";
        
        private const string CarName_IntegrationTest01 = "CarFromIntegrationTest01";
        private const string PilotName_IntegrationTest01 = "PilotFromIntegrationTest01";
        
        private const string Metric_position13_CarClass = "pitwall_leaderboard_position13_carclass";
        private const string Metric_position13_carNumber = "pitwall_leaderboard_position13_carnumber";

        private FakeLeaderboardModel _actualModel;

        private LeaderboardSqlRepository GetTarget()
        {
            return new LeaderboardSqlRepository(new FakeConnectionString(SqlConnectionString));
        }

        public LeaderboardSqlRepositoryTest()
        {
            CleanUp();

            InsertOneData();
        }

        private void InsertOneData()
        {
            var model = new FakeLeaderboardModel()
            {
                CarName = new Models.CarName(CarName_IntegrationTest01),
                PilotName = new Models.PilotName(PilotName_IntegrationTest01),
            };

            var entry = new FakeBusinessEntry()
            {
                CarClass = "GTE",
                CarNumber = "53",
                LastPitLap = 12,
                Position = EntryPosition
            };

            model.AddEntry(entry);

            var target = GetTarget();

            target.Update(model);

            _actualModel = model;
        }

        [Fact]
        public void GIVEN_entry_AND_pilotName_AND_carName_THEN_insert_metric_car_class()
        {
            // ARRANGE
            var actual = GetFrom(_actualModel);

            var actualCarClass = ReadMetric(actual, Metric_position13_CarClass);

            var actualCarNumber = ReadMetric(actual, Metric_position13_carNumber);

            Check.That(actualCarClass.metric_value).IsEqualTo("GTE");

            Check.That(actualCarNumber.metric_value).IsEqualTo("53");
        }

        private static ActualLeaderboardRow? ReadMetric(IEnumerable<ActualLeaderboardRow> actual, string metricName)
        {
            return actual.FirstOrDefault(c =>
                c.car_name == CarName_IntegrationTest01
                && c.pilot_name == PilotName_IntegrationTest01
                && c.metric_name == metricName);
        }

        private static void CleanUp()
        {
            using (var connection = new MySqlConnection(SqlConnectionString))
            {
                var sql = "DELETE FROM metric_leaderboard  WHERE pilot_name = @pilot_name AND car_name = @car_name";

                _ = connection.Query<ActualLeaderboardRow>(sql, new
                {
                    pilot_name = PilotName_IntegrationTest01,
                    car_name = CarName_IntegrationTest01
                });
            }
        }

        private IEnumerable<ActualLeaderboardRow> GetFrom(FakeLeaderboardModel original)
        {
            using (var connection = new MySqlConnection(SqlConnectionString))
            {
                var sql = "SELECT * FROM metric_leaderboard WHERE pilot_name = @pilot_name AND car_name = @car_name";

                var entries = connection.Query<ActualLeaderboardRow>(sql, new
                {
                    pilot_name = PilotName_IntegrationTest01,
                    car_name = CarName_IntegrationTest01
                }).ToList();

                return entries;
            }
        }
    }
}
