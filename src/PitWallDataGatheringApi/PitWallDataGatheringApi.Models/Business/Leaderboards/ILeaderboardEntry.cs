namespace PitWallDataGatheringApi.Models.Business.Leaderboards
{
    public interface ILeaderboardEntry
    {
        public int LastPitLap
        {
            get;
        }
        string CarClass
        {
            get;
        }

        string CarNumber { get; }

        int Position { get; }
        
        bool InPitLane { get; }

        string PilotName { get; }

        public string CarName { get; }
    }
}