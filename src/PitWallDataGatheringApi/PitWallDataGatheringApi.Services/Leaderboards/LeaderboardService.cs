using PitWallDataGatheringApi.Models.Business.Leaderboards;
using PitWallDataGatheringApi.Repositories.Leaderboards.Updates;

namespace PitWallDataGatheringApi.Services.Leaderboards
{
    public sealed class LeaderboardService(
        ILeaderboardRepository leaderboardCarNumberRepository,
        ILeaderboardLivetimingSqlRepository leaderboardLivetiming)
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

            leaderboardCarNumberRepository.Update(leaderboardModel);

            leaderboardLivetiming.Update(leaderboardModel);

            foreach (var entry in leaderboardModel)
            {
                
            }
        }
    }
}
