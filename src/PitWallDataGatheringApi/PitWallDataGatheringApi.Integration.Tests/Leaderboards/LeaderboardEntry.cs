using ZstdSharp.Unsafe;

namespace PitWallDataGatheringApi.Integration.Tests.Leaderboards
{
    public class LeaderboardEntry
    {
        public int LastPitLap { get; set; }

        public string CarClass { get; set; } = String.Empty;

        public string CarNumber { get; set; } = String.Empty;

        public int Position { get; set; }
    }
}
