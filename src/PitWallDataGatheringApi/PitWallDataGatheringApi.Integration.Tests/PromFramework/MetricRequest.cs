namespace PitWallDataGatheringApi.Integration.Tests.PromFramework;

public struct MetricRequest
{
    public MetricRequest(string metricName, string label, string labelValue)
    {
        MetricName = metricName;
        Label = label;
        LabelValue = labelValue;
    }

    public string LabelValue { get; }

    public string Label { get;  }

    public string MetricName { get; }
}