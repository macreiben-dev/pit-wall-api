namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public interface ITrackTemperatureRepository : IMetricRepositoryLegacy, IMetricRepository<double?>
    {
    }
}