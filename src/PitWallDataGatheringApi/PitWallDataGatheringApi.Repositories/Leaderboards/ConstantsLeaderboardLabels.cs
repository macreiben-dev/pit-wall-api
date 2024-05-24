namespace PitWallDataGatheringApi.Repositories.Leaderboards;

public static class ConstantsLeaderboardLabels
{
    public static readonly IEnumerable<string> Labels = new[] { SourcePilotLabel, SourceCarLabel };
    private const string SourcePilotLabel = "SourcePilot";
    private const string SourceCarLabel = "SourceCar";
}