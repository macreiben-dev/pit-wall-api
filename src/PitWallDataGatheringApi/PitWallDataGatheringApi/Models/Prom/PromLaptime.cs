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
            var pilot = BuildPilot(laptimeMilliseconds);

            switch (pilotName)
            {
                case "Pilot1": 
                    _promPilots.Pilot1 = pilot;
                    break;
                case "Pilot2":
                    _promPilots.Pilot2 = pilot;
                    break;

            }

            _laptimeMilliseconds = laptimeMilliseconds;
        }

        private PromPilot BuildPilot(double laptimeMilliseconds)
        {
            return new PromPilot()
            {
                LaptimeMilliseconds = laptimeMilliseconds
            };
        }
    }
}
