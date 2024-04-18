using PitWallDataGatheringApi.Models;

namespace PitWallDataGatheringApi.Integration.Tests.Leaderboards;

public struct LabelValues
{
    public PilotName Pilot { get; }
    public CarName Car { get; }

    public LabelValues(PilotName pilot,
        CarName car)
    {
        Pilot = pilot;
        Car = car;
    }
}