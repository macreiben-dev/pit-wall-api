namespace PitWallDataGatheringApi.Repositories.Prom
{
    public interface IGaugeFactory
    {
        /// <summary>
        /// Creates or retrieve existing serie from the serie name.
        /// </summary>
        /// <param name="serieName">The name of the serie.</param>
        /// <param name="description">The description for the documentation entry point.</param>
        /// <param name="labels">The labels to add to the gauge.</param>
        /// <returns>one <see cref="IGauge"/></returns>
        IGauge Create(
            string serieName,
            string description,
            IEnumerable<string> labels);

        /// <summary>
        /// List all created series.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ISerieDocumentation> ListCreated();
    }
}
