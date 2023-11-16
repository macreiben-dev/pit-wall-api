using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Repositories.Tyres;
using PitWallDataGatheringApi.Repositories.VehicleConsumptions;
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
            IAirTemperatureRepository airTemperature, 
            ITrackTemperatureRepository trackTemperatureRepository,
            IComputedLastLapConsumptionRepository laspLapConsumptionRepository,
            IComputedLiterPerLapsRepository literPerLapsRepository,
            IComputedRemainingLapsRepository remainingLaps,
            IComputedRemainingTimeRepository lapConsumptionRepository,
            IFuelRepository fuelRepository,
            IMaxFuelRepository maxFuelRepository)
        {
            Target = target;
            TyreWearRepository = tyreWearRepository;
            TyreTemperature = tyreTemperature;
            LaptimeRepository = laptimeRepository;
            AvgWetnessRepository = wetnessRepository;
            AirTemperature = airTemperature;
            TrackTemperature = trackTemperatureRepository;
            LaspLapConsumptionRepository = laspLapConsumptionRepository;
            LiterPerLapsRepository = literPerLapsRepository;
            RemainingLaps = remainingLaps;
            LapConsumptionRepository = lapConsumptionRepository;
            FuelRepository = fuelRepository;
            MaxFuelRepository = maxFuelRepository;
        }

        public PitwallTelemetryService Target { get; }
        public ITyreWearRepository TyreWearRepository { get; }
        public ITyresTemperaturesRepository TyreTemperature { get; }
        public ILaptimeRepository LaptimeRepository { get; }
        public IAvgWetnessRepository AvgWetnessRepository { get; }
        public IAirTemperatureRepository AirTemperature { get; }
        public ITrackTemperatureRepository TrackTemperature { get; }
        public IComputedLastLapConsumptionRepository LaspLapConsumptionRepository { get; }
        public IComputedLiterPerLapsRepository LiterPerLapsRepository { get; }
        public IComputedRemainingLapsRepository RemainingLaps { get; }
        public IComputedRemainingTimeRepository LapConsumptionRepository { get; }
        public IFuelRepository FuelRepository { get; }
        public IMaxFuelRepository MaxFuelRepository { get; }
    }
}