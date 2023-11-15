namespace PitWallDataGatheringApi.Repositories.Prometheus
{
    public interface ISerieDocumentation
    {
        public string SerieName { get; }

        public IEnumerable<string> Labels { get; }

        public string Description { get; }
    }
}
