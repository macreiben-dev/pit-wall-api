namespace PitWallDataGatheringApi.Models.Prom
{
    public class PromPilots : IPromPilots
    {
        public PromPilots()
        {
            Pilot1 = new PromPilot
            {
                LaptimeMilliseconds = 0
            };

            Pilot2 = new PromPilot
            {
                LaptimeMilliseconds = 0
            };
        }

        public PromPilot Pilot1 { get; set; }

        public PromPilot Pilot2 { get; set; }
    }
}