using PitWallDataGatheringApi.Models.Business.Leaderboards;
using PitWallDataGatheringApi.Repositories.Leaderboards;

namespace PitWallDataGatheringApi.Services.Leaderboards
{
    public sealed class LeaderboardService : ILeaderBoardService
    {
        private ILeaderboardRepository _leaderboardRepo;

        public LeaderboardService(ILeaderboardRepository leaderboardCarNumberRepository)
        {
            _leaderboardRepo = leaderboardCarNumberRepository;
        }

        public void Update(ILeaderboardModel leaderboardModel)
        {
            if(leaderboardModel == null)
            {
                throw new ArgumentNullException(nameof(leaderboardModel));
            }

            _leaderboardRepo.Update(leaderboardModel);
        }
    }
}
