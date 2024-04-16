using PitWallDataGatheringApi.Models.Business.Leaderboards;

namespace PitwallDataGatheringApi.Tests.BusinessCommon.Business;

public class FakeBusinessEntry : ILeaderboardEntry
{
    public FakeBusinessEntry()
    {
        CarClass = string.Empty;
        CarNumber = string.Empty;
        
        PilotName = string.Empty;
        CarName = string.Empty;
    }

    public int LastPitLap { get; set; }

    public string CarClass { get; set; }

    public string CarNumber { get; set; }

    public int Position { get; set; }

    public bool InPitLane { get; set; }
    
    public bool InPitBox { get; set; }
    
    public string PilotName { get; set; }
    
    public string CarName { get; set; }

}
