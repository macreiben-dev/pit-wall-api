using PitWallDataGatheringApi.Models.Business.Leaderboards;

namespace PitWallDataGatheringApi.Repositories.Leaderboards
{
    public interface ILeaderboardRepository
    {
        void Update(ILeaderboardModel model);
    }
}