namespace PitWallDataGatheringApi.Repositories.Prometheus
{
    public interface IGaugeWrapperFactory
    {
        IGauge Create(
            string serieName,
            string description,
            IEnumerable<string> labels);
    }
}
