namespace PitWallDataGatheringApi.Models.Apis.v1;

public interface ICallerInfos
{

    string? PilotName { get; }

    string? CarName { get; }

    string SimerKey
    {
        get;
    }
}