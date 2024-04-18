using PitWallDataGatheringApi.Models.Business.Leaderboards;

namespace PitWallDataGatheringApi.Services.Leaderboards
{
    public interface ILeaderBoardService
    {
        void Update(ILeaderboardModel leaderboardModel);

        IEnumerable<ILeaderboardReadEntry> Get(string pilotName, string carName);
        void ClearLiveTiming();
    }
}