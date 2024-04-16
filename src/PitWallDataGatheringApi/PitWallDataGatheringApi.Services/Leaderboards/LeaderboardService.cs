using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Models.Business.Leaderboards;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Repositories.Leaderboards;
using PitWallDataGatheringApi.Repositories.Leaderboards.Updates;

namespace PitWallDataGatheringApi.Services.Leaderboards
{
    public sealed class LeaderboardService(
        ILeaderboardLivetimingSqlRepository leaderboardLivetiming,
        ILeaderboardPitlaneRepository pitlaneRepository)
        : ILeaderBoardService
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
                    entry.InPitLane ?  1.0 : 0.0, 
                    new PilotName(entry.PilotName), 
                    new CarName(entry.CarName)));
            }
            
        }
    }
}
