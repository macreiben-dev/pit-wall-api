namespace PitWallDataGatheringApi.Repositories.Prometheus
{
    public sealed class GaugeWrapperFactory : IGaugeWrapperFactory
    {
        public IGauge Create(
            string serieName,
            string description,
            string[] labels)
        {
            return new GaugeWrapper(serieName, description, labels);
        }
    }
}
