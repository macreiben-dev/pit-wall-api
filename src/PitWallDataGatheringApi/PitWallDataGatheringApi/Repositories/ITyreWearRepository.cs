using PitWallDataGatheringApi.Models.Apis;

namespace PitWallDataGatheringApi.Repositories
{
    public interface ITyreWearRepository
    {
        void UpdateFrontLeft(ITyres? tyresWears);
        void UpdateFrontRight(ITyres? tyresWears);
        void UpdateRearLeft(ITyres? tyresWears);
        void UpdateRearRight(ITyres? tyresWears);
    }
}