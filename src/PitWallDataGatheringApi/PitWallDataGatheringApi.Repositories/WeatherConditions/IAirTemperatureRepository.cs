namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public interface IAirTemperatureRepository : IMetricRepositoryLegacy, IMetricRepository<double?>
    {
    }
}