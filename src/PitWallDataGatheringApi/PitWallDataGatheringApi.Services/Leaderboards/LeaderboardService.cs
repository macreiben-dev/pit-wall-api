using PitWallDataGatheringApi.Models.Business.Leaderboards;
using PitWallDataGatheringApi.Repositories.Leaderboards;

namespace PitWallDataGatheringApi.Services.Leaderboards
{
    public sealed class LeaderboardService : ILeaderBoardService
    {
        private ILeaderboardRepository _leaderboardRepo;
        private ILeaderboardLivetimingSqlRepository _leaderboardLivetimingRepo;

        public LeaderboardService(ILeaderboardRepository leaderboardCarNumberRepository, ILeaderboardLivetimingSqlRepository leaderboardLivetiming)
        {
            _leaderboardRepo = leaderboardCarNumberRepository;

            _leaderboardLivetimingRepo = leaderboardLivetiming;
        }

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

            _leaderboardRepo.Update(leaderboardModel);

            _leaderboardLivetimingRepo.Update(leaderboardModel);
        }
    }
}
