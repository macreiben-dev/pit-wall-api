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
        int CarNumber { get; }
    }
}