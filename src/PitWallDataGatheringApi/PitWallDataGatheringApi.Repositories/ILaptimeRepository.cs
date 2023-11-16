using PitWallDataGatheringApi.Models;

namespace PitWallDataGatheringApi.Repositories
{
    public interface ILaptimeRepository
    {
        void Update(double? laptime, string pilotName, CarName carName);
    }
}