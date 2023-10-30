using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Repositories.Tyres;
using PitWallDataGatheringApi.Repositories.WeatherConditions;
using PitWallDataGatheringApi.Services;

namespace PitWallDataGatheringApi.Tests.Services
{
    public class TestContextPitwallTelemetryService
    {
        public TestContextPitwallTelemetryService(PitwallTelemetryService target, 
            ITyreWearRepository tyreWearRepository, 
            ITyresTemperaturesRepository tyreTemperature, 
            ILaptimeRepository laptimeRepository,
            IAvgWetnessRepository wetnessRepository,
            IAirTemperatureRepository airTemperature)
        {
            Target = target;
            TyreWearRepository = tyreWearRepository;
            TyreTemperature = tyreTemperature;
            LaptimeRepository = laptimeRepository;
            AvgWetnessRepository = wetnessRepository;
            AirTemperature = airTemperature;
        }

        public PitwallTelemetryService Target { get; }
        public ITyreWearRepository TyreWearRepository { get; }
        public ITyresTemperaturesRepository TyreTemperature { get; }
        public ILaptimeRepository LaptimeRepository { get; }
        public IAvgWetnessRepository AvgWetnessRepository { get; }
        public IAirTemperatureRepository AirTemperature { get; }
    }
}