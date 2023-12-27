using PitWallDataGatheringApi.Models.Business.Leaderboards;

namespace PitWallDataGatheringApi.Services
{
    public interface ILeaderBoardService
    {
        void Update(ILeaderboardModel leaderboardModel);
    }
}