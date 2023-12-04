using PitWallDataGatheringApi.Models;

namespace PitWallDataGatheringApi.Repositories.Tyres
{
    public interface ITyreWearRepositoryLegacy : ITyreWearRepositoryV2
    {
        void UpdateFrontLeft(double? frontLeftWear, string pilotName, CarName carName);

        void UpdateFrontRight(double? frontRightWear, string pilotName, CarName carName);

        void UpdateRearLeft(double? reartLeftWear, string pilotName, CarName carName);

        void UpdateRearRight(double? rearRightWear, string pilotName, CarName carName);
    }
}