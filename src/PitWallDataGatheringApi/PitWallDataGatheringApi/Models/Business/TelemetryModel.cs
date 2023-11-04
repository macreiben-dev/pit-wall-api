namespace PitWallDataGatheringApi.Models.Business
{
    public sealed class TelemetryModel : ITelemetryModel
    {
        public TelemetryModel()
        {
            TyresWear = new TyresWear();
            TyresTemperatures = new TyresTemperatures();
        }
        public string PilotName { get; set; }
        public double? LaptimeSeconds { get; set; }

        public double? AirTemperature { get; set; }

        public double? AvgWetness { get; set; }

        public ITyresWear TyresWear { get; set; }

        public ITyresTemperatures TyresTemperatures { get; set; }
    }
}