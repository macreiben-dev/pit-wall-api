namespace PitWallDataGatheringApi.Repositories.Prometheus
{
    public interface IGaugeWrapperFactory
    {
        IGauge Create(
            string serieName,
            string description,
            string[] labels);
    }
}
