namespace PitWallDataGatheringApi.Repositories.Prom
{
    public interface IGaugeWrapperFactory
    {
        IGauge Create(
            string serieName,
            string description,
            IEnumerable<string> labels);

        IEnumerable<ISerieDocumentation> ListCreated();
    }
}
