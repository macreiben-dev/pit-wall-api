namespace PitWallDataGatheringApi.Models.Business;

public interface ITyresWear
{
    double? FrontLeftWear { get; }
    double? FrontRightWear { get; }
    double? RearRightWear { get; }
    double? ReartLeftWear { get; }
}