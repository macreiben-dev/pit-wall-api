namespace PitWallDataGatheringApi.Models.Apis.v1
{
    public class LeaderboardModel : ICallerInfos
    {
        public string? PilotName { get; set; }

        public string? CarName { get; set; }

        public string SimerKey { get; set; }

        public IEnumerable<LeaderboardEntry> Entries { get; set; }
    }
}
