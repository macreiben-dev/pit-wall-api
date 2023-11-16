namespace PitWallDataGatheringApi.Models.Apis
{
    public class VehicleConsumption
    {
        public double? Fuel { get; set; }

        public double? MaxFuel { get; set; }

        public double? LastLapConsumption { get; set; }

        public double? ComputedLiterPerLaps { get; set; }

        public double? ComputedRemainingLaps { get; set; }

        public double? ComputedRemainingTime { get; set; }
    }
}
