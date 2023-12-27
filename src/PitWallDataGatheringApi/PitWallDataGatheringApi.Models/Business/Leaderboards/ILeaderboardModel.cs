namespace PitWallDataGatheringApi.Models.Business.Leaderboards
{
    public interface ILeaderboardModel : IEnumerable<ILeaderboardEntry>
    {
        PilotName PilotName { get; }

        CarName CarName { get; }
    }
}