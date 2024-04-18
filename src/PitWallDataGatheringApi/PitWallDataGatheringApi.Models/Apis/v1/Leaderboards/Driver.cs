namespace PitWallDataGatheringApi.Models.Apis.v1.Leaderboards;

public class Driver : ICallerInfos
{
    public string? PilotName { get; set; }
    public string? CarName { get; set; }
    public string SimerKey { get; set; }
}