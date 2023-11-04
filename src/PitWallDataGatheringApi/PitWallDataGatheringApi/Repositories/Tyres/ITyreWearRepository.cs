using PitWallDataGatheringApi.Models.Business;

namespace PitWallDataGatheringApi.Repositories.Tyres
{
    public interface ITyreWearRepository
    {
        void UpdateFrontLeft(string pilotName, double? frontLeftWear);
        
        void UpdateFrontRight(string pilotName, double? frontRightWear);
        
        void UpdateRearLeft(string pilotName, double? reartLeftWear);

        void UpdateRearRight(string pilotName, double? rearRightWear);
    }
}