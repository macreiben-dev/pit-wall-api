namespace PitWallDataGatheringApi.Models.Apis.v1
{
    public class VehicleConsumption
    {
        public double? Fuel { get; set; }

        public double? MaxFuel { get; set; }

        public double? ComputedLastLapConsumption { get; set; }

        public double? ComputedLiterPerLaps { get; set; }

        public double? ComputedRemainingLaps { get; set; }

        public double? ComputedRemainingTime { get; set; }
    }
}
