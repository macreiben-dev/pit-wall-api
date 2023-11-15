using PitWallDataGatheringApi.Repositories.Prometheus;

namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public interface ITrackTemperatureRepository
    {
        void Update(double? dataValue, string pilotName, CarName carName);
    }
}