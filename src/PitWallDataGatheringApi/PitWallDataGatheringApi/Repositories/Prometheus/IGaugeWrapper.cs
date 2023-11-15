namespace PitWallDataGatheringApi.Repositories.Prometheus
{
    public interface IGauge
    {
        void Update(string label, double? value);

        string Description { get; }

        string SerieName { get; }

        IEnumerable<string> Labels { get; }
    }
}
