using Microsoft.Extensions.Logging;
using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Models.Business.Leaderboards;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Repositories.Leaderboards;

namespace PitWallDataGatheringApi.Services.Leaderboards
{
    public sealed class LeaderboardService(
        ILeaderboardPitlaneRepository pitlaneRepository,
        ILeaderboardInPitBoxRepository pitBoxRepository,
        ILogger<LeaderboardService> logger) : ILeaderBoardService
    {
        public void Update(ILeaderboardModel leaderboardModel)
        {
            if(leaderboardModel == null)
            {
                throw new ArgumentNullException(nameof(leaderboardModel));
            }
            
            foreach (var entry in leaderboardModel)
            {
                pitlaneRepository.Update(new MetricDataWithSource<double?>(
                    BoolToMetricValue(entry.InPitLane), 
                    new PilotName(entry.PilotName), 
                    new CarName(entry.CarName),
                    leaderboardModel.PilotName,
                    leaderboardModel.CarName)
                );

                pitBoxRepository.Update(new MetricDataWithSource<double?>(
                    BoolToMetricValue(entry.InPitBox),
                    new PilotName(entry.PilotName),
                    new CarName(entry.CarName),
                    leaderboardModel.PilotName,
                    leaderboardModel.CarName)
                );
            }
        }

        private static double BoolToMetricValue(bool entryInPitBox)
        {
            return entryInPitBox ? 1.0 : 0.0;
        }
    }
}
