﻿using Microsoft.Extensions.Logging;
using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Models.Business.Leaderboards;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Repositories.Leaderboards;
using PitWallDataGatheringApi.Repositories.Leaderboards.Updates;

namespace PitWallDataGatheringApi.Services.Leaderboards
{
    public sealed class LeaderboardService(
        ILeaderboardLivetimingSqlRepository leaderboardLivetiming,
        ILeaderboardPitlaneRepository pitlaneRepository,
        ILeaderboardInPitBoxRepository pitBoxRepository, 
        ILogger<LeaderboardService> logger) : ILeaderBoardService
    {
        public IEnumerable<ILeaderboardReadEntry> Get(string pilotName, string carName)
        {
            throw new NotImplementedException();
        }

        public void Update(ILeaderboardModel leaderboardModel)
        {
            if(leaderboardModel == null)
            {
                throw new ArgumentNullException(nameof(leaderboardModel));
            }
            
            leaderboardLivetiming.Update(leaderboardModel);

            foreach (var entry in leaderboardModel)
            {
                pitlaneRepository.Update(new MetricData<double?>(
                    BoolToMetricValue(entry.InPitLane), 
                    new PilotName(entry.PilotName), 
                    new CarName(entry.CarName)));

                pitBoxRepository.Update(new MetricData<double?>(
                    BoolToMetricValue(entry.InPitBox),
                    new PilotName(entry.PilotName),
                    new CarName(entry.CarName)));

                logger.LogInformation($"Entry {nameof(entry.PilotName)}:[{entry.PilotName}] " +
                                      $"- {nameof(entry.CarName)}:[{entry.CarName}] " +
                                      $"- {nameof(entry.InPitLane)}:[{entry.InPitLane}]");
                
                logger.LogInformation($"Entry {nameof(entry.PilotName)}:[{entry.PilotName}] " +
                                      $"- {nameof(entry.CarName)}:[{entry.CarName}] " +
                                      $"- {nameof(entry.InPitLane)}:[{entry.InPitBox}]");
            }
        }

        private static double BoolToMetricValue(bool entryInPitBox)
        {
            return entryInPitBox ? 1.0 : 0.0;
        }
    }
}
