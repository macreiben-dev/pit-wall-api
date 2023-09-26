namespace PitWallDataGatheringApi.Models.Apis
{
    public sealed class Tyres
    {
        public double FrontLeftWear { get; set; }
        public string? FrontLeftType { get; set; }

        public double FrontRightWear { get; set; }
        public string? FrontRightType { get; set; }

        public double ReartLeftWear { get; set; }
        public string? RearLeftType { get; set; }

        public double RearRightWear { get; set; }
        public string? RearRightType { get; set; }
    }
}