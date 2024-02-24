using PitWallDataGatheringApi.Models.Business.Leaderboards;

namespace PitWallDataGatheringApi.Repositories.Leaderboards.Updates
{
    public interface ILeaderboardLivetimingSqlRepository
    {
        void Update(ILeaderboardModel model);
    }
}