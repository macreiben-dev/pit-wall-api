using PitWallDataGatheringApi.Models.Business.Leaderboards;
using PitWallDataGatheringApi.Repositories.Leaderboards;

namespace PitWallDataGatheringApi.Services.Leaderboards
{
    public sealed class LeaderboardService : ILeaderBoardService
    {
        private ILeaderboardCarNumberRepository _carNumberRepo;

        public LeaderboardService(ILeaderboardCarNumberRepository leaderboardCarNumberRepository)
        {
            _carNumberRepo = leaderboardCarNumberRepository;
        }

        public void Update(ILeaderboardModel leaderboardModel)
        {
            if(leaderboardModel == null)
            {
                throw new ArgumentNullException(nameof(leaderboardModel));
            }

            foreach(var entry in leaderboardModel)
            {
                _carNumberRepo.Update(
                    entry, 
                    leaderboardModel.PilotName, 
                    leaderboardModel.CarName);
            }
        }
    }
}
