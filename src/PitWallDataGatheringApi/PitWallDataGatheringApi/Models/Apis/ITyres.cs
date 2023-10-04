namespace PitWallDataGatheringApi.Models.Apis
{
    public interface ITyres
    {
        double? FrontLeftWear { get; }
        double? FrontRightWear { get; }
        double? RearRightWear { get; }
        double? ReartLeftWear { get; }
    }
}