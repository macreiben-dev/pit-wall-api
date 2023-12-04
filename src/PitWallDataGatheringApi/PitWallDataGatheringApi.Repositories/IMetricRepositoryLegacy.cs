using PitWallDataGatheringApi.Models;

namespace PitWallDataGatheringApi.Repositories
{
    public interface IMetricRepositoryLegacy
    {
        void Update(double? laptime, string pilotName, CarName carName);
    }
}