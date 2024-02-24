using PitWallDataGatheringApi.Services.Leaderboards;

namespace PitWallDataGatheringApi.Repositories.Leaderboards.Reads
{
    public interface IReadLeaderboardRepository
    {
        IEnumerable<ILeaderboardReadEntry> Get(string pilotName, string carName);
    }
}