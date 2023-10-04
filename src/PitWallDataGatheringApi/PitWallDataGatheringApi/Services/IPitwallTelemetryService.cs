using PitWallDataGatheringApi.Models.Apis;
using PitWallDataGatheringApi.Models.Business;

namespace PitWallDataGatheringApi.Services
{
    public interface IPitwallTelemetryService
    {
        /// <summary>
        /// Orchestrater the metric update
        /// </summary>
        /// <param name="telemetry"></param>
        void Update(ITelemetryModel telemetry);
    }
}