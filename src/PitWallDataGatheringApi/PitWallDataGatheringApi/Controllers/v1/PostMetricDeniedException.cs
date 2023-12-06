using PitWallDataGatheringApi.Models.Apis.v1;

namespace PitWallDataGatheringApi.Controllers.v1
{
    [Serializable]
    internal class PostMetricDeniedException : Exception
    {
        public PostMetricDeniedException(ICallerInfos telemetry) :
            base($"Post metric denied for [{telemetry.PilotName}] - [{telemetry.CarName}], AccessKey [{telemetry.SimerKey}].")
        {
            this.Telemetry = telemetry;
        }

        public ICallerInfos Telemetry { get; }
    }
}