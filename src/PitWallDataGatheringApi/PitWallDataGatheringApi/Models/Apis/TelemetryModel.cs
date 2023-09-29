namespace PitWallDataGatheringApi.Models.Apis
{
    public sealed class TelemetryModel
    {
        public TelemetryModel()
        {
            Tyres = new Tyres();
        }
        public string PilotName { get; set; }
        public int LaptimeMilliseconds { get; set; }

        public Tyres Tyres { get; set; }
    }
}