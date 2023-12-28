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

            _carNumberRepo.Update(leaderboardModel.First(), leaderboardModel.PilotName, leaderboardModel.CarName);
        }
    }
}
