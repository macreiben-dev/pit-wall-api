using PitWallDataGatheringApi.Models.Apis.v1;

namespace PitWallDataGatheringApi.Integration.Tests
{
    public sealed class TelemetryContext : ITelemetryContext
    {
        public string _simerKey = null;

        public TelemetryContext(string simerKey)
        {
            _simerKey = simerKey;
            MetricName = null;
            PilotName = null;
        }

        public string? MetricName { get; set; }

        public string SimerKey => _simerKey;

        public string? PilotName { get; set; }

        public object Expected { get; set; }

        public Action<TelemetryModel> SetFieldValue { get; set; }

        public Func<TelemetryModel> GetApiModelInstance { get; set; }

        public string CarName
        {
            get; set;
        }

        public override string ToString()
        {
            return $"Metricname: [{MetricName}] - PilotName: [{PilotName}] - CarName: [{CarName}] - Expected: [{Expected}]";
        }
    }
}