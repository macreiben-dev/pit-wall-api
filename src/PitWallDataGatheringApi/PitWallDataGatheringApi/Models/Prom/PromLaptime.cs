namespace PitWallDataGatheringApi.Models.Prom
{
    public sealed class PromLapData
    {
        private double _laptimeMilliseconds;

        public double LaptimeMilliseconds => _laptimeMilliseconds;

        public PromLapData WithLaptimeMilliseconds(double laptimeMilliseconds)
        {
            _laptimeMilliseconds = laptimeMilliseconds;

            return this;
        }
    }
}
