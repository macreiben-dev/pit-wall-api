using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Models.Business.Leaderboards;
using PitWallDataGatheringApi.Repositories.Prom;

namespace PitWallDataGatheringApi.Repositories.Leaderboards
{
    public interface ILeaderboardCarNumberRepository
    {
        void Update(ILeaderboardEntry entry, PilotName pilotName, CarName carName);
    }
}