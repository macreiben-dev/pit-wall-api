using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Repositories.Tyres;
using PitWallDataGatheringApi.Services;

namespace PitWallDataGatheringApi.Tests.Services
{
    public class TestContextPitwallTelemetryService
    {
        public TestContextPitwallTelemetryService(PitwallTelemetryService target, ITyreWearRepository tyreWearRepository, ITyresTemperaturesRepository tyreTemperature, ILaptimeRepository laptimeRepository)
        {
            Target = target;
            TyreWearRepository = tyreWearRepository;
            TyreTemperature = tyreTemperature;
            LaptimeRepository = laptimeRepository;
        }

        public PitwallTelemetryService Target { get; }
        public ITyreWearRepository TyreWearRepository { get; }
        public ITyresTemperaturesRepository TyreTemperature { get; }
        public ILaptimeRepository LaptimeRepository { get; }
    }
}