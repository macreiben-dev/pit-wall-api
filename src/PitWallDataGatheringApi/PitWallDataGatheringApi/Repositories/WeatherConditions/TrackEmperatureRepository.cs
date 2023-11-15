namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public sealed class TrackEmperatureRepository : ITrackTemperatureRepository, IDocumentationTrackTemperatureSerie
    {
        public string SerieName => throw new NotImplementedException();

        public string[] Labels => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();
    }
}
