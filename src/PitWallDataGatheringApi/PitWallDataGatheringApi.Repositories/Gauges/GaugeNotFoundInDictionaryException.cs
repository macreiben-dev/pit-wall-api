namespace PitWallDataGatheringApi.Repositories.Gauges;

public class GaugeNotFoundInDictionaryException : Exception
{
    public GaugeNotFoundInDictionaryException(KeyNotFoundException source, IEnumerable<string> actualGauges, string requestSerieName)
    : base($"Gauge with serie name {requestSerieName} not found in dictionary. Actual gauges: {string.Join(", ", actualGauges)}", source)
    {
        AvailableGauges = actualGauges;
        RequestedSerieName = requestSerieName;
    }

    public IEnumerable<string> AvailableGauges { get;  }

    public string RequestedSerieName { get;  }
}