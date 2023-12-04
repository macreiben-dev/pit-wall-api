using Microsoft.Extensions.Logging;
using NSubstitute;
using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Models.Business;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Repositories.Tyres;
using PitWallDataGatheringApi.Repositories.VehicleConsumptions;
using PitWallDataGatheringApi.Repositories.WeatherConditions;
using PitWallDataGatheringApi.Services;

namespace PitWallDataGatheringApi.Tests.Services
{
    public sealed partial class PitwallTelemetryServiceTest
    {
        private readonly ILogger<PitwallTelemetryService> _logger;
        private readonly ITyreWearRepositoryLegacy _tyreWearRepository;
        private readonly ILaptimeRepository _laptimeRepository;
        private readonly ITyresTemperaturesRepositoryLegacy _tyreTemperature;
        private readonly IAvgWetnessRepository _avgWetness;
        private readonly IAirTemperatureRepository _airTemperature;
        private readonly ITrackTemperatureRepository _trackTemperature;
        private readonly IComputedLastLapConsumptionRepository _lastLapConsumption;
        private readonly IComputedLiterPerLapsRepository _literPerLap;
        private readonly IFuelRepository _fuel;
        private readonly IMaxFuelRepository _maxFuel;
        private IComputedRemainingLapsRepository _remainingLaps;
        private readonly IComputedRemainingTimeRepository _remainingTime;
        private readonly PilotName PilotName = new PilotName("Pilot01");
        private readonly CarName CarName = new CarName("SomeCarName");

        public PitwallTelemetryServiceTest()
        {
            _logger = Substitute.For<ILogger<PitwallTelemetryService>>();

            _tyreWearRepository = Substitute.For<ITyreWearRepositoryLegacy>();

            _laptimeRepository = Substitute.For<ILaptimeRepository>();

            _tyreTemperature = Substitute.For<ITyresTemperaturesRepositoryLegacy>();

            _avgWetness = Substitute.For<IAvgWetnessRepository>();

            _airTemperature = Substitute.For<IAirTemperatureRepository>();

            _trackTemperature = Substitute.For<ITrackTemperatureRepository>();

            _lastLapConsumption = Substitute.For<IComputedLastLapConsumptionRepository>();
            _literPerLap = Substitute.For<IComputedLiterPerLapsRepository>();
            _remainingLaps = Substitute.For<IComputedRemainingLapsRepository>();
            _remainingTime = Substitute.For<IComputedRemainingTimeRepository>();
            _fuel = Substitute.For<IFuelRepository>();
            _maxFuel = Substitute.For<IMaxFuelRepository>();
        }

        private PitwallTelemetryService GetTarget()
        {
            return new PitwallTelemetryService(
                _tyreWearRepository,
                _laptimeRepository,
                _tyreTemperature,
                _avgWetness,
                _airTemperature,
                _trackTemperature,
                _lastLapConsumption,
                _literPerLap,
                _remainingLaps,
                _remainingTime,
                _fuel,
                _maxFuel,
                _logger);
        }

        private TelemetryModel Create()
        {
            return new TelemetryModel()
            {
                PilotName = PilotName,
                CarName = CarName,
            };
        }
        public static TestContextPitwallTelemetryService GetTargetTestContext()
        {
            var tyreWearRepository = Substitute.For<ITyreWearRepositoryLegacy>();

            var laptimeRepository = Substitute.For<ILaptimeRepository>();

            var tyreTemperature = Substitute.For<ITyresTemperaturesRepositoryLegacy>();

            var avgWetnessRepository = Substitute.For<IAvgWetnessRepository>();

            var airTemperature = Substitute.For<IAirTemperatureRepository>();

            var trackTemperature = Substitute.For<ITrackTemperatureRepository>();

            var lastLapConsumption = Substitute.For<IComputedLastLapConsumptionRepository>();
            var literPerLap = Substitute.For<IComputedLiterPerLapsRepository>();
            var remainingLaps = Substitute.For<IComputedRemainingLapsRepository>();
            var remainingTime = Substitute.For<IComputedRemainingTimeRepository>();
            var fuel = Substitute.For<IFuelRepository>();
            var maxFuel = Substitute.For<IMaxFuelRepository>();

            var target = new PitwallTelemetryService(
                tyreWearRepository,
                laptimeRepository,
                tyreTemperature,
                avgWetnessRepository,
                airTemperature,
                trackTemperature,
                lastLapConsumption,
               literPerLap,
               remainingLaps,
               remainingTime,
               fuel,
               maxFuel,
               Substitute.For<ILogger<PitwallTelemetryService>>());

            return new TestContextPitwallTelemetryService(
                target,
                tyreWearRepository,
                tyreTemperature,
                laptimeRepository,
                avgWetnessRepository,
                airTemperature,
                trackTemperature,
                lastLapConsumption,
               literPerLap,
               remainingLaps,
               remainingTime,
               fuel,
               maxFuel);
        }

        [Fact]
        public void GIVEN_telemetry_isNull_THEN_should_not_updateTyres()
        {
            var target = GetTarget();

            // ACT
            target.Update(null);

            // ASSERT
            _tyreWearRepository.Received(0).UpdateFrontLeft(Arg.Any<double?>() , PilotName, CarName.Null());
            _tyreWearRepository.Received(0).UpdateFrontRight(Arg.Any<double?>(), PilotName, CarName.Null());
            _tyreWearRepository.Received(0).UpdateRearLeft(Arg.Any<double?>(), PilotName, CarName.Null());
            _tyreWearRepository.Received(0).UpdateRearRight(Arg.Any<double?>(), PilotName, CarName.Null());
        }

        [Fact]
        public void GIVEN_telemetry_isNull_THEN_should_not_update_laptimes()
        {
            var target = GetTarget();

            // ACT
            target.Update(null);

            // ASSERT
            _laptimeRepository.Received(0).Update(Arg.Any<MetricData<double?>>());
        }

        [Fact]
        public void GIVEN_telemetry_isNotNull_THEN_update_laptimes()
        {
            TelemetryModel source = Create();

            source.LaptimeSeconds = 10.0;

            EnsureCalledWithValue(source, _laptimeRepository);
        }


        [Fact]
        public void GIVEN_telemetry_isNull_THEN_should_not_update_avgWetness()
        {
            var target = GetTarget();

            // ACT
            target.Update(null);

            // ASSERT
            _avgWetness.Received(0).Update(Arg.Any<double?>(), Arg.Any<string>(), Arg.Any<CarName>());
        }

        [Fact]
        public void GIVEN_telemetry_isNotNull_THEN_update_avgWetness()
        {
            TelemetryModel source = Create();

            source.AvgWetness = 10.0;

            EnsureCalledWithValue(source, _avgWetness);
        }

        [Fact]
        public void GIVEN_telemetry_isNull_THEN_should_not_update_airTemperature()
        {
            var target = GetTarget();

            // ACT
            target.Update(null);

            // ASSERT
            _airTemperature.Received(0).Update(Arg.Any<double?>(), Arg.Any<string>(), Arg.Any<CarName>());
        }


        [Fact]
        public void GIVEN_telemetry_isNotNull_THEN_update_airTemperature()
        {
            TelemetryModel source = Create();

            source.AirTemperature = 10.0;

            EnsureCalledWithValue(source, _airTemperature);
        }

        [Fact]
        public void GIVEN_telemetry_isNull_THEN_should_not_update_trackTemperature()
        {
            var target = GetTarget();

            // ACT
            target.Update(null);

            // ASSERT
            _trackTemperature.Received(0).Update(Arg.Any<double?>(), Arg.Any<string>(), new CarName(null));
        }

        [Fact]
        public void GIVEN_telemetry_isNotNull_THEN_update_trackTemperature()
        {
            TelemetryModel source = Create();

            source.TrackTemperature = 10.0;

            EnsureCalledWithValue(source, _trackTemperature);
        }


        private void EnsureCalledWithValue(TelemetryModel source, IMetricRepositoryLegacy metricRepository)
        {
            var target = GetTarget();

            // ACT
            target.Update(source);

            // ASSERT
            metricRepository.Received(1).Update(10.0, PilotName, CarName);
        }
    }
}
