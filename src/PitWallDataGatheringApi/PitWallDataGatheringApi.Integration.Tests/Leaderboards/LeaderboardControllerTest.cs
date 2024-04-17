using Dapper;
using MySql.Data.MySqlClient;
using NFluent;
using System.Net.Http.Json;

namespace PitWallDataGatheringApi.Integration.Tests.Leaderboards
{
    public class LeaderboardControllerTest : IAsyncLifetime
    {
        private readonly Uri _leaderboardUri;
        private readonly string _connectionString;

        private const string AudiR8Lms = "Audi R8 LMS";
        private const string JohnDoe = "John Doe";
        
        private const string SimerKey = "some_test_looking_value23";

        private const string CarClassGt3 = "GT3-metrics";
        private const string CarNumber01 = "#1 test livetiming";
        private const int Position01 = 1;
        private const int LastPitLap10 = 10;

        private IEnumerable<LeaderboardReadData> _actual;

        public LeaderboardControllerTest()
        {
            _leaderboardUri = new Uri("http://localhost:32773/api/v1/leaderboard");
            _connectionString = "Server=localhost;Database=pitwall_leaderboard;User=root;Password=password;";
        }

        [Fact]
        public async void GIVEN_pitwall_leaderboard_THEN_persisted_metric_count_is_valid()
        {
            Check.That(_actual.Count()).IsEqualTo(1);
        }

        [Fact]
        public async void GIVEN_pitwall_leaderboard_THEN_persisted_metric_carClass_is_valid()
        {
            var actual = _actual.First();

            Check.That(actual.metric_car_class).IsEqualTo(CarClassGt3);
        }

        [Fact]
        public async void GIVEN_pitwall_leaderboard_THEN_persisted_metric_carNumber_isValid()
        {
            var actual = _actual.First();

            Check.That(actual.metric_car_number).IsEqualTo(CarNumber01);
        }
        
        private async Task<IEnumerable<LeaderboardReadData>> PostMetricAndReturnInsertedData(LeaderboardModelBuilder leaderboardModelBuilder)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(_leaderboardUri, leaderboardModelBuilder.Build());

                response.EnsureSuccessStatusCode();
            }

            IEnumerable<LeaderboardReadData> actual = Enumerable.Empty<LeaderboardReadData>();

            await using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                actual = connection.Query<LeaderboardReadData>(@$"
                            SELECT * 
                            FROM metric_leaderboard_livetiming 
                            WHERE 1=1 
                            AND metric_car_number = '{CarNumber01}'
                            AND metric_car_class = '{CarClassGt3}'
                            AND metric_position = '{Position01}'
                            ");
            }

            return actual;
        }

        public async Task InitializeAsync()
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Execute($"DELETE FROM metric_leaderboard_livetiming WHERE source_pilot_name = '{JohnDoe}'");
            }

            var entryBuilder = new LeaderboardEntryBuilder()
                 .WithLastPitLap(LastPitLap10)
                 .WithCarClass(CarClassGt3)
                 .WithCarNumber(CarNumber01)
                 .WithPosition(Position01);

            var leaderboardModelBuilder = new LeaderboardModelBuilder(
                    AudiR8Lms,
                    JohnDoe,
                    SimerKey)
                .WithEntry(entryBuilder.Build());

            _actual = await PostMetricAndReturnInsertedData(leaderboardModelBuilder);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
