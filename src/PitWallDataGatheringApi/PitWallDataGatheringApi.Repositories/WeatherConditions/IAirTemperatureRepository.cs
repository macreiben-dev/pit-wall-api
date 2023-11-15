using PitWallDataGatheringApi.Models;

namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public interface IAirTemperatureRepository
    {
        void Update(double? laptime, string pilotName, CarName carName);
    }
}