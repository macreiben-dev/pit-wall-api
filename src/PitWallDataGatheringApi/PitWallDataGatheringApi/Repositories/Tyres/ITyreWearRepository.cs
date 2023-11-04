using PitWallDataGatheringApi.Models.Business;

namespace PitWallDataGatheringApi.Repositories.Tyres
{
    public interface ITyreWearRepository
    {
        void UpdateFrontLeft(ITyresWear? tyresWears, string pilotName);
        void UpdateFrontRight(ITyresWear? tyresWears, string pilotName);
        void UpdateRearLeft(ITyresWear? tyresWears, string pilotName);
        void UpdateRearRight(ITyresWear? tyresWears, string pilotName);
    }
}