namespace PitWallDataGatheringApi.Models.Business
{
    public interface IVehicleConsumption
    {
        double? Fuel { get; }

        double? MaxFuel
        {
            get;
        }

        double? ComputedLastLapConsumption { get; }

        public double? ComputedLiterPerLaps { get; }

        public double? ComputedRemainingLaps { get; }

        public double? ComputedRemainingTime { get; }
    }

    public sealed class VehicleConsumption : IVehicleConsumption
    {
        public double? Fuel { get; set; }

        public double? MaxFuel { get; set; }

        public double? ComputedLastLapConsumption { get; set; }

        public double? ComputedLiterPerLaps { get; set; }

        public double? ComputedRemainingLaps { get; set; }

        public double? ComputedRemainingTime { get; set; }
    }
}
