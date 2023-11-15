using PitWallDataGatheringApi.Models.Business;

namespace PitWallDataGatheringApi.Repositories.Tyres
{
    public interface ITyresTemperaturesRepository
    {
        void UpdateFrontLeft(string pilotName, double? frontLeftTemp);

        void UpdateFrontRight(string pilotName, double? frontRightTemp);

        void UpdateRearLeft(string pilotName, double? rearLeftTemp);

        void UpdateRearRight(string pilotName, double? rearRightTemp);
    }
}