using PitWallDataGatheringApi.Models.Business;

namespace PitWallDataGatheringApi.Repositories.Tyres
{
    public interface ITyresTemperaturesRepository
    {
        void UpdateFrontLeft(ITyresTemperatures? data, string pilotName);
        void UpdateFrontRight(ITyresTemperatures? data, string pilotName);
        void UpdateRearLeft(ITyresTemperatures? data, string pilotName);
        void UpdateRearRight(ITyresTemperatures? data, string pilotName);
    }
}