using PitWallDataGatheringApi.Models;

namespace PitWallDataGatheringApi.Repositories.Tyres
{
    public interface ITyresTemperaturesRepositoryLegacy : ITyresTemperaturesRepository
    {
        void UpdateFrontLeft(double? frontLeftTemp, string pilotName, CarName carName);

        void UpdateFrontRight(double? frontRightTemp, string pilotName, CarName carName);

        void UpdateRearLeft(double? rearLeftTemp, string pilotName, CarName carName);

        void UpdateRearRight(double? rearRightTemp, string pilotName, CarName carName);
    }
}