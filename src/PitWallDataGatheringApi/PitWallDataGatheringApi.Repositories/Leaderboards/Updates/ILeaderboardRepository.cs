using PitWallDataGatheringApi.Models.Business.Leaderboards;

namespace PitWallDataGatheringApi.Repositories.Leaderboards.Updates
{
    public interface ILeaderboardRepository
    {
        void Update(ILeaderboardModel model);
    }
}