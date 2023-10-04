namespace PitWallDataGatheringApi.Models.Apis
{
    public sealed class TelemetryModel
    {
        public TelemetryModel()
        {
            TyresWear = new TyresWear();
        }
        public string PilotName { get; set; }

        public double? LaptimeSeconds { get; set; }

        public TyresWear? TyresWear { get; set; }

        public TyresTemperatures? TyresTemperatures { get; set; }
    }
}