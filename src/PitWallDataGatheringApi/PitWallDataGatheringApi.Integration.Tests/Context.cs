using PitWallDataGatheringApi.Models.Apis.v1;

namespace PitWallDataGatheringApi.Integration.Tests
{
    public sealed class Context : IContext
    {
        public string _simerKey = null;

        public Context(string simerKey)
        {
            _simerKey = simerKey;
        }

        public string? MetricName { get; set; }

        public string SimerKey => _simerKey;

        public string? PilotName { get; set; }

        public object OriginalValue { get; set; }

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