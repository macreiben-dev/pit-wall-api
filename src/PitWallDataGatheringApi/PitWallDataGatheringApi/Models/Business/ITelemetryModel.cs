namespace PitWallDataGatheringApi.Models.Business
{
    public interface ITelemetryModel
    {
        double? LaptimeSeconds { get; }

        string PilotName { get; }

        public double? AirTemperature { get; }

        public double? AvgWetness { get; }

        ITyresWear TyresWear { get; }

        ITyresTemperatures TyresTemperatures { get; }
    }
}