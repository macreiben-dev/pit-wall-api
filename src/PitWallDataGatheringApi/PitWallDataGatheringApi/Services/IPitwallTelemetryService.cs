using PitWallDataGatheringApi.Models.Apis;

namespace PitWallDataGatheringApi.Services
{
    public interface IPitwallTelemetryService
    {
        void Update(TelemetryModel telemetry);
    }
}