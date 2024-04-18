
namespace PitWallDataGatheringApi.Models.Apis.v1.Leaderboards
{
    public sealed class LeaderboardModel : ICallerInfos
    {
        public string? PilotName { get; set; }

        public string? CarName { get; set; }

        public required string SimerKey { get; set; }

        public IList<LeaderboardEntry> Entries { get; set; } = new List<LeaderboardEntry>();
    }
}
