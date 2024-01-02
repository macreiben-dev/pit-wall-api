using PitWallDataGatheringApi.Models.Apis.v1;

namespace PitWallDataGatheringApi.Integration.Tests
{
    public interface ITelemetryContext
    {
        object Expected { get; }
        string MetricName { get; }
        string PilotName { get; }
        string CarName { get; }
        string SimerKey { get; }

        Action<TelemetryModel> SetFieldValue
        {
            get;
        }
        Func<TelemetryModel> GetApiModelInstance
        {
            get;
        }
    }
}