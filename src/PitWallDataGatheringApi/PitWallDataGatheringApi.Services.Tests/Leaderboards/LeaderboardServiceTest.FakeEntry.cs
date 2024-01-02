using PitWallDataGatheringApi.Models.Business.Leaderboards;

namespace PitWallDataGatheringApi.Services.Tests.Leaderboards
{
    public class FakeEntry : ILeaderboardEntry
    {
        public int LastPitLap { get; set; }

        public string CarClass { get; set; }

        public int CarNumber { get; set; }

        public int Position { get; set; }
    }
}
