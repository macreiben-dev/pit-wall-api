using PitWallDataGatheringApi.Models.Apis.v1;

namespace PitWallDataGatheringApi.Services
{
    public interface IAuthenticatePayloadService
    {
        IEnumerable<string> ValidatePayload(ICallerInfos telemetry);
    }
}