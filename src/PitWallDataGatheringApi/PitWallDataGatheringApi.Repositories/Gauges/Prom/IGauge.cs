namespace PitWallDataGatheringApi.Repositories.Gauges.Prom
{
    public interface IGauge
    {
        void Update(string label, double? value);

        void Update(IEnumerable<string> labels, double? dataValue);

        string Description { get; }

        string SerieName { get; }

        IEnumerable<string> Labels { get; }
    }
}
