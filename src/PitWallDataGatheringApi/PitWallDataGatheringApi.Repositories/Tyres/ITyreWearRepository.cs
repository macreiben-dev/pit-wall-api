using PitWallDataGatheringApi.Models;

namespace PitWallDataGatheringApi.Repositories.Tyres
{
    public interface ITyreWearRepository
    {
        void UpdateFrontLeft(string pilotName, double? frontLeftWear, CarName carName);

        void UpdateFrontRight(string pilotName, double? frontRightWear, CarName carName);

        void UpdateRearLeft(string pilotName, double? reartLeftWear, CarName carName);

        void UpdateRearRight(string pilotName, double? rearRightWear, CarName carName);
    }
}