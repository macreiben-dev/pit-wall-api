namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public interface IAirTemperatureRepository : IMetricRepository, IMetricRepositoryV2<double?>
    {
    }
}