using PitWallDataGatheringApi.Models.Business.Leaderboards;

namespace PitWallDataGatheringApi.Repositories.Tests.LeaderboardCarNumberRepositories
{
    internal class FakeBusinessEntry : ILeaderboardEntry
    {
        public int LastPitLap { get; set; }

        public string CarClass { get; set; }

        public string CarNumber { get; set; }

        public int Position { get; set; }

        public bool InPitLane { get; set; }
    }
}
