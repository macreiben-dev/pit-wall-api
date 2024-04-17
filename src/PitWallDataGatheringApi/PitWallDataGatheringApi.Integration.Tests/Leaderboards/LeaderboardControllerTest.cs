﻿using Dapper;
using MySql.Data.MySqlClient;
using NFluent;
using System.Net.Http.Json;

namespace PitWallDataGatheringApi.Integration.Tests.Leaderboards
{
    public class LeaderboardControllerTest : IAsyncLifetime
    {
        private readonly Uri _leaderboardUri;
        private readonly string _connectionString;

        private const string AudiR8LMS = "Audi R8 LMS";
        private const string JohnDoe = "John Doe";

        private const string Position1CarClass = "pitwall_leaderboard_position01_carclass";
        private const string Position1CarNumber = "pitwall_leaderboard_position01_carnumber";
        private const string SimerKey = "some_test_looking_value23";

        private const string CarClassGt3 = "GT3-metrics";
        private const string CarNumber01 = "1";
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
            Check.That(_actual.Count()).IsEqualTo(2);
        }

        [Fact]
        public async void GIVEN_pitwall_leaderboard_THEN_persisted_metric_carClass_is_valid()
        {
            var actual = _actual.First(c => c.metric_name == Position1CarClass);

            Check.That(actual.metric_value).IsEqualTo(CarClassGt3);
        }

        [Fact]
        public async void GIVEN_pitwall_leaderboard_THEN_persisted_metric_carNumber_isValid()
        {
            var actual = _actual.First(c => c.metric_name == Position1CarNumber);

            Check.That(actual.metric_value).IsEqualTo(CarNumber01);
        }
        
        private async Task<IEnumerable<LeaderboardReadData>> PostMetricAndReturnInsertedData(LeaderboardModelBuilder leaderboardModelBuilder)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(_leaderboardUri, leaderboardModelBuilder.Build());

                response.EnsureSuccessStatusCode();
            }

            IEnumerable<LeaderboardReadData> actual = Enumerable.Empty<LeaderboardReadData>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                actual = connection.Query<LeaderboardReadData>(@$"
                            SELECT * 
                            FROM metric_leaderboard_livetiming 
                            WHERE 1=1 
                            AND metric_name IN (
                                '{Position1CarClass}', 
                                '{Position1CarNumber}')
                            AND pilot_name = '{JohnDoe}'
                            AND car_name = '{AudiR8LMS}'    
                            ");
            }

            return actual;
        }

        public async Task InitializeAsync()
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Execute($"DELETE FROM metric_leaderboard WHERE pilot_name = '{JohnDoe}'");
            }

            var entryBuilder = new LeaderboardEntryBuilder()
                 .WithLastPitLap(LastPitLap10)
                 .WithCarClass(CarClassGt3)
                 .WithCarNumber(CarNumber01)
                 .WithPosition(Position01);

            var leaderboardModelBuilder = new LeaderboardModelBuilder(
                    AudiR8LMS,
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
