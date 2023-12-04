using PitWallDataGatheringApi.Models;

namespace PitWallDataGatheringApi.Repositories.Tyres
{
    public interface ITyresTemperaturesRepositoryLegacy
    {
        void UpdateFrontLeft(string pilotName, double? frontLeftTemp, CarName carName);

        void UpdateFrontRight(string pilotName, double? frontRightTemp, CarName carName);

        void UpdateRearLeft(string pilotName, double? rearLeftTemp, CarName carName);

        void UpdateRearRight(string pilotName, double? rearRightTemp, CarName carName);
    }
}