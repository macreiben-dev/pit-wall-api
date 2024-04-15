﻿using MySql.Data.MySqlClient;
using PitWallDataGatheringApi.Models.Business.Leaderboards;
using PitWallDataGatheringApi.Repositories.Gauges.Sql;
using PitWallDataGatheringApi.Repositories.Leaderboards.CommandBuildes;
using System.Data;

namespace PitWallDataGatheringApi.Repositories.Leaderboards.Updates
{
    public sealed class LeaderboardSqlRepository : ILeaderboardRepository
    {
        private const string SerieNameCarNumberFormat = "pitwall_leaderboard_position{0}_carnumber";
        private const string SerieNameCarClassFormat = "pitwall_leaderboard_position{0}_carclass";

        private const string Sql =
            @"INSERT INTO pitwall_leaderboard.metric_leaderboard(pilot_name, car_name, data_tick, metric_name, metric_value) 
                    VALUES(@pilot_name, @car_name, @data_tick, @metric_name, @metric_value)";

        private readonly ILeaderboardConnectionString _connectionString;

        public LeaderboardSqlRepository(ILeaderboardConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public void Update(ILeaderboardModel model)
        {
            var actualTick = DateTime.Now.Ticks;

            using MySqlConnection connection = new MySqlConnection(_connectionString.ToString());
            connection.Open();

            using var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            
            foreach (var entry in model)
            {
                {
                    var commandBuilder = new DbMetricCommandBuilder(model, actualTick, connection, Sql);

                    commandBuilder.WithMetricNameFormat(SerieNameCarNumberFormat)
                        .AndPositionValue(entry.Position)
                        .WithMetricValue(entry.CarNumber);

                    commandBuilder
                        .AsCommand()
                        .ExecuteNonQuery();
                }

                {
                    var commandBuilder = new DbMetricCommandBuilder(model, actualTick, connection, Sql);

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
