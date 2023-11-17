using PitWallDataGatheringApi.Models;

namespace PitWallDataGatheringApi.Repositories
{
    public interface IMetricRepository
    {
        void Update(double? laptime, string pilotName, CarName carName);
    }
}