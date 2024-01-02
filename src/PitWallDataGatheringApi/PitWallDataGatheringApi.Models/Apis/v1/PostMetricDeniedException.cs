namespace PitWallDataGatheringApi.Models.Apis.v1
{
    [Serializable]
    public class PostMetricDeniedException : Exception
    {
        public PostMetricDeniedException(ICallerInfos telemetry) :
            base($"Post metric denied for [{telemetry.PilotName}] - [{telemetry.CarName}], AccessKey [{telemetry.SimerKey}].")
        {
            Telemetry = telemetry;
        }

        public ICallerInfos Telemetry { get; }
    }
}