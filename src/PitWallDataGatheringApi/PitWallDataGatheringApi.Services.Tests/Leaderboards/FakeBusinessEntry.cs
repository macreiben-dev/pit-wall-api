using PitWallDataGatheringApi.Models.Business.Leaderboards;

namespace PitWallDataGatheringApi.Services.Tests.Leaderboards
{
    public class FakeBusinessEntry : ILeaderboardEntry
    {
        public FakeBusinessEntry()
        {
            CarClass = string.Empty;
            CarNumber = string.Empty;
        }

        public int LastPitLap { get; set; }

        public string CarClass { get; set; }

        public string CarNumber { get; set; }

        public int Position { get; set; }

        public bool InPitLane { get; set; }
    }
}
