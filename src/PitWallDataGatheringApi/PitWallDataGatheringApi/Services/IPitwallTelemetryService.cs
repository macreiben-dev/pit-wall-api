using PitWallDataGatheringApi.Models.Apis;

namespace PitWallDataGatheringApi.Services
{
    public interface IPitwallTelemetryService
    {
        /// <summary>
        /// Orchestrater the metric update
        /// </summary>
        /// <param name="telemetry"></param>
        void Update(TelemetryModel telemetry);
    }
}