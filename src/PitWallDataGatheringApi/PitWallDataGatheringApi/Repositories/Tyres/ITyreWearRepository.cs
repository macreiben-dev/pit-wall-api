using PitWallDataGatheringApi.Models.Business;

namespace PitWallDataGatheringApi.Repositories.Tyres
{
    public interface ITyreWearRepository
    {
        void UpdateFrontLeft(ITyresWear? tyresWears);
        void UpdateFrontRight(ITyresWear? tyresWears);
        void UpdateRearLeft(ITyresWear? tyresWears);
        void UpdateRearRight(ITyresWear? tyresWears);
    }
}