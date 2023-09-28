namespace PitWallDataGatheringApi.Models.Prom
{
    public sealed class PromLapData
    {
        private double _laptimeMilliseconds;
        private PromPilots _promPilots;

        public double LaptimeMilliseconds => _laptimeMilliseconds;

        public PromLapData()
        {
            _promPilots = new PromPilots();
        }

        public IPromPilots Pilots => _promPilots;

        public PromLapData WithLaptimeMilliseconds(double laptimeMilliseconds)
        {
            _laptimeMilliseconds = laptimeMilliseconds;

            return this;
        }

        public void WithPilotLaptime(string pilotName, int laptimeMilliseconds)
        {
            PromPilot pilot = new PromPilot()
            {
                LaptimeMilliseconds = laptimeMilliseconds
            };

            _promPilots.Pilot1 = pilot;
        }
    }
}
