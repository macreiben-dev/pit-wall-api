using PitWallDataGatheringApi.Services.Leaderboards;

namespace PitWallDataGatheringApi.Repositories.Leaderboards
{
    public interface IReadLeaderboardRepository
    {
        IEnumerable<ILeaderboardReadEntry> Get(string pilotName, string carName);
    }
}