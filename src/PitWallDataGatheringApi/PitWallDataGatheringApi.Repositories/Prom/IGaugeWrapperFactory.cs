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
        /// Creates a gauge according to a formated name. The gauge is stored and returned each time
        /// it is asked to avoid multiple recreation.
        /// </summary>
        /// <param name="serieNameFormat">The name of the serie with one placeholder.</param>
        /// <param name="description">The description of the serie.</param>
        /// <param name="positionInRace">The position of the entry.</param>
        /// <param name="labels">The labels of the gauges/></param>
        /// <returns></returns>
        IGauge CreateLeaderboardGauge(
            string serieNameFormat,
            string description,
            int positionInRace,
            IEnumerable<string> labels);

        /// <summary>
        /// List all created series.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ISerieDocumentation> ListCreated();
    }
}
