namespace PitWallDataGatheringApi.Repositories.Prometheus
{
    public interface IGauge
    {
        void Update(string label, double? value);

        void Update(string[] labels, double? dataValue);

        string Description { get; }

        string SerieName { get; }

        IEnumerable<string> Labels { get; }
    }
}
