namespace PitWallDataGatheringApi.Models.Apis
{
    public interface ITelemetryModel
    {
        double? LaptimeSeconds { get; }
        string PilotName { get; }
        ITyres? Tyres { get; }
    }
}