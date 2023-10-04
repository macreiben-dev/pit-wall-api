namespace PitWallDataGatheringApi.Models.Business
{
    public interface ITelemetryModel
    {
        double? LaptimeSeconds { get; }
        string PilotName { get; }
        ITyresWear? TyresWear { get; }
    }
}