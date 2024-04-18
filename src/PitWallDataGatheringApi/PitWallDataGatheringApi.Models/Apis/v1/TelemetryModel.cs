namespace PitWallDataGatheringApi.Models.Apis.v1;

public sealed class TelemetryModel : ICallerInfos
{
    public TelemetryModel()
    {
        TyresWear = new TyresWear();
        SimerKey = String.Empty;
    }

    public string? PilotName { get; set; }

    public string? CarName { get; set; }

    public double? LaptimeSeconds { get; set; }

    public TyresWear? TyresWear { get; set; }

    public TyresTemperatures? TyresTemperatures { get; set; }

    public VehicleConsumption? VehicleConsumption { get; set; }

    public string SimerKey { get; set; }

    public double? AvgWetness { get; set; }

    public double? AirTemperature { get; set; }

    public double? TrackTemperature { get; set; }
}
