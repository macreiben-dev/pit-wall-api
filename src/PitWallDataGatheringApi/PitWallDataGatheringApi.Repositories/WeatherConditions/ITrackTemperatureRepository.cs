namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public interface ITrackTemperatureRepository : IMetricRepository, IMetricRepositoryV2<double?>
    {
    }
}