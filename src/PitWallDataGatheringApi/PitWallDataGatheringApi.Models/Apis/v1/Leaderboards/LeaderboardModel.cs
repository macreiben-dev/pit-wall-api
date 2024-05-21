using Swashbuckle.AspNetCore.Annotations;

namespace PitWallDataGatheringApi.Models.Apis.v1.Leaderboards
{
    [SwaggerSchema(Required = ["Description"], Description = "The leaderboard model.")]
    public sealed class LeaderboardModel : ICallerInfos
    {
        [SwaggerSchema("The source pilot name.")]
        public string? PilotName { get; set; }

        [SwaggerSchema("The source pilot name.")]
        public string? CarName { get; set; }

        [SwaggerSchema("The simerkey.", Nullable = false)]
        public required string SimerKey { get; set; }

        [SwaggerSchema("The entries informations.")]
        public IList<LeaderboardEntry> Entries { get; set; } = new List<LeaderboardEntry>();
    }
}