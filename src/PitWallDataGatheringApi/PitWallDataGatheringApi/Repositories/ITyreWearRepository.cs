using PitWallDataGatheringApi.Models.Apis;

namespace PitWallDataGatheringApi.Repositories
{
    public interface ITyreWearRepository
    {
        void UpdateFrontLeft(Tyres? tyresWears);
        void UpdateFrontRight(Tyres? tyresWears);
        void UpdateRearLeft(Tyres? tyresWears);
        void UpdateRearRight(Tyres? tyresWears);
    }
}