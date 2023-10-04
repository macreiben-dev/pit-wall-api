namespace PitWallDataGatheringApi.Models.Apis
{
    public sealed class TelemetryModel : ITelemetryModel
    {
        public TelemetryModel()
        {
            Tyres = new Tyres();
        }
        public string PilotName { get; set; }
        public double? LaptimeSeconds { get; set; }

        public ITyres? Tyres { get; set; }
    }
}