using PitWallDataGatheringApi.Models;

namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public interface IAvgWetnessRepository
    {
        void Update(double? laptime, string pilotName, CarName carName);
    }
}