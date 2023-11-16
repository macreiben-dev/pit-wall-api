namespace PitWallDataGatheringApi.Models.Business;

public interface ITelemetryModel
{
    double? LaptimeSeconds { get; }

    string PilotName { get; }

    double? AirTemperature { get; }

    double? AvgWetness { get; }

    double? TrackTemperature { get; }

    ITyresWear TyresWear { get; }

    ITyresTemperatures TyresTemperatures { get; }

    IVehicleConsumption VehicleConsumption
    {
        get;
    }
}