namespace PitWallDataGatheringApi.Models.Business.Leaderboards
{
    public class LeaderboardEntry : ILeaderboardEntry
    {
        private const string NotAvailable = "NotAvailable";
        public int LastPitLap { get; set; }

        public string CarClass { get; set; } 

        public string CarNumber { get; set; }

        public int Position { get; set; }

        public bool InPitLane { get; set; }
        
        public bool InPitBox { get; set; }
        
        public string PilotName { get; set; } = NotAvailable;

        public string CarName { get; set; } = NotAvailable;
    }
}
