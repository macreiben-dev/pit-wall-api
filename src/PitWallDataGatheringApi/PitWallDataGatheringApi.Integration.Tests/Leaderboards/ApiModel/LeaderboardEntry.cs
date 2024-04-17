namespace PitWallDataGatheringApi.Integration.Tests.Leaderboards.ApiModel
{
    public class LeaderboardEntry
    {
        public int LastPitLap { get; set; }

        public string CarClass { get; set; } = String.Empty;

        public string CarNumber { get; set; } = String.Empty;

        public int Position { get; set; }
        
        public bool InPitBox { get; set; }
        
        public bool InPitLane { get; set; }
        
        public string? CarName { get; set; }
    }
}
