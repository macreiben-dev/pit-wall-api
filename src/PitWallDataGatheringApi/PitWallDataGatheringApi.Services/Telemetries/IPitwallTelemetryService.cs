using PitWallDataGatheringApi.Models.Business;

namespace PitWallDataGatheringApi.Services.Telemetries
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