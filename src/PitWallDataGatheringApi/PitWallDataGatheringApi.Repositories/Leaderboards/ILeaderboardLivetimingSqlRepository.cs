using PitWallDataGatheringApi.Models.Business.Leaderboards;

namespace PitWallDataGatheringApi.Repositories.Leaderboards
{
    public interface ILeaderboardLivetimingSqlRepository
    {
        void Update(ILeaderboardModel model);
    }
}