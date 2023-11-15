namespace PitWallDataGatheringApi.Models
{
    public struct CarName
    {
        private string carName;

        public CarName(string? carName) => this.carName = carName ?? "NoCarNumber";

        public override string ToString()
        {
            return carName;
        }
    }
}
