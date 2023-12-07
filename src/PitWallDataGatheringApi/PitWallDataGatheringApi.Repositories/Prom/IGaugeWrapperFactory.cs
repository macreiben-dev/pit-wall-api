namespace PitWallDataGatheringApi.Repositories.Prom
{
    public interface IGaugeFactory
    {
        IGauge Create(
            string serieName,
            string description,
            IEnumerable<string> labels);

        IEnumerable<ISerieDocumentation> ListCreated();
    }
}
