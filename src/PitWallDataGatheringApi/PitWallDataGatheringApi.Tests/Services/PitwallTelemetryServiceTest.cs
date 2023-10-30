using NSubstitute;
using PitWallDataGatheringApi.Models.Business;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Repositories.Tyres;
using PitWallDataGatheringApi.Repositories.WeatherConditions;
using PitWallDataGatheringApi.Services;

namespace PitWallDataGatheringApi.Tests.Services
{
    public class PitwallTelemetryServiceTest
    {
        private readonly ITyreWearRepository _tyreWearRepository;
        private readonly ILaptimeRepository _laptimeRepository;
        private readonly ITyresTemperaturesRepository _tyreTemperature;

        public PitwallTelemetryServiceTest()
        {
            _tyreWearRepository = Substitute.For<ITyreWearRepository>();

            _laptimeRepository = Substitute.For<ILaptimeRepository>();

            _tyreTemperature = Substitute.For<ITyresTemperaturesRepository>();
        }

        private PitwallTelemetryService GetTarget()
        {
            return new PitwallTelemetryService(
                _tyreWearRepository,
                _laptimeRepository,
                _tyreTemperature,
                Substitute.For<IAvgWetnessRepository>(), 
                Substitute.For<IAirTemperatureRepository>());
        }

        public static TestContextPitwallTelemetryService GetTargetTestContext()
        {
            var tyreWearRepository = Substitute.For<ITyreWearRepository>();

            var laptimeRepository = Substitute.For<ILaptimeRepository>();

            var tyreTemperature = Substitute.For<ITyresTemperaturesRepository>();

            var avgWetnessRepository = Substitute.For<IAvgWetnessRepository>();

            var airTemperature = Substitute.For<IAirTemperatureRepository>();

            var target = new PitwallTelemetryService(
                tyreWearRepository,
                laptimeRepository,
                tyreTemperature,
                avgWetnessRepository, 
                airTemperature);

            return new TestContextPitwallTelemetryService(
                target, 
                tyreWearRepository, 
                tyreTemperature, 
                laptimeRepository, 
                avgWetnessRepository,
                airTemperature);
        }

        [Fact]
        public void GIVEN_telemetry_isNull_THEN_should_not_updateTyres()
        {
            var target = GetTarget();

            // ACT
            target.Update(null);

            // ASSERT
            _tyreWearRepository.Received(0).UpdateFrontLeft(Arg.Any<ITyresWear>());
            _tyreWearRepository.Received(0).UpdateFrontRight(Arg.Any<ITyresWear>());
            _tyreWearRepository.Received(0).UpdateRearLeft(Arg.Any<ITyresWear>());
            _tyreWearRepository.Received(0).UpdateRearRight(Arg.Any<ITyresWear>());
        }

        [Fact]
        public void GIVEN_telemetry_isNull_THEN_should_not_update_laptimes()
        {
            var target = GetTarget();

            // ACT
            target.Update(null);

            // ASSERT
            _laptimeRepository.Received(0).Update(Arg.Any<double?>(), Arg.Any<string>());
        }

        public class TyreWearTest
        {
            private TestContextPitwallTelemetryService _context;

            public TyreWearTest()
            {
                _context = GetTargetTestContext();
            }

            [Fact]
            public void GIVEN_telemetry_with_TyresWear_isNull_THEN_should_not_updateTyres()
            {
                var original = new TelemetryModel();

                original.TyresWear = null;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreWearRepository.Received(0).UpdateFrontLeft(Arg.Any<ITyresWear>());
                _context.TyreWearRepository.Received(0).UpdateFrontRight(Arg.Any<ITyresWear>());
                _context.TyreWearRepository.Received(0).UpdateRearLeft(Arg.Any<ITyresWear>());
                _context.TyreWearRepository.Received(0).UpdateRearRight(Arg.Any<ITyresWear>());
            }

            [Fact]
            public void GIVEN_telemetry_with_tyreWear_THEN_updateFrontLeft()
            {
                // ARRANGE
                var original = new TelemetryModel();
                var tyres = new TyresWear()
                {
                    FrontLeftWear = 50.0
                };

                original.TyresWear = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreWearRepository.Received(1).UpdateFrontLeft(
                    Arg.Is<TyresWear>(c => c.FrontLeftWear == 50.0));
            }

            [Fact]
            public void GIVEN_telemetry_with_tyreWear_THEN_updateFrontRight()
            {
                // ARRANGE
                var original = new TelemetryModel();
                var tyres = new TyresWear()
                {
                    FrontRightWear = 51.0
                };

                original.TyresWear = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreWearRepository.Received(1).UpdateFrontRight(
                    Arg.Is<TyresWear>(c => c.FrontRightWear == 51.0));
            }

            [Fact]
            public void GIVEN_telemetry_with_tyreWear_THEN_updateRearLeft()
            {
                // ARRANGE
                var original = new TelemetryModel();

                var tyres = new TyresWear()
                {
                    ReartLeftWear = 52.0
                };

                original.TyresWear = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreWearRepository.Received(1).UpdateRearLeft(
                    Arg.Is<TyresWear>(c => c.ReartLeftWear == 52.0));
            }

            [Fact]
            public void GIVEN_telemetry_with_tyreWear_THEN_updateRearRight()
            {
                // ARRANGE
                var original = new TelemetryModel();

                var tyres = new TyresWear()
                {
                    RearRightWear = 53.0
                };

                original.TyresWear = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreWearRepository.Received(1).UpdateRearRight(
                    Arg.Is<TyresWear>(c => c.RearRightWear == 53.0));
            }
        }

        public class TyresTemperaturesTest
        {
            private TestContextPitwallTelemetryService _context;

            private const string PilotName = "pilot01";

            public TyresTemperaturesTest()
            {
                _context = GetTargetTestContext();
            }

            [Fact]
            public void GIVEN_telemetry_with_TyresTemperatyres_isNull_THEN_should_not_updateTyres()
            {
                var original = new TelemetryModel();
                original.PilotName = PilotName;
                original.TyresWear = null;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreTemperature.Received(0).UpdateFrontLeft(Arg.Any<ITyresTemperatures>(), PilotName);
                _context.TyreTemperature.Received(0).UpdateFrontRight(Arg.Any<ITyresTemperatures>(), PilotName);
                _context.TyreTemperature.Received(0).UpdateRearLeft(Arg.Any<ITyresTemperatures>(), PilotName);
                _context.TyreTemperature.Received(0).UpdateRearRight(Arg.Any<ITyresTemperatures>(), PilotName);
            }

            [Fact]
            public void GIVEN_telemetry_with_tyreTemperature_THEN_updateFrontLeft()
            {
                // ARRANGE
                var original = new TelemetryModel();
                var tyres = new TyresTemperatures()
                {
                    FrontLeftTemp = 50.0
                };

                original.TyresTemperatures = tyres;
                original.PilotName = PilotName;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreTemperature.Received(1).UpdateFrontLeft(
                    Arg.Is<TyresTemperatures>(c => c.FrontLeftTemp == 50.0), PilotName);
            }

            [Fact]
            public void GIVEN_telemetry_with_tyreTemperature_THEN_updateFrontRight()
            {
                // ARRANGE
                var original = new TelemetryModel();
                var tyres = new TyresTemperatures()
                {
                    FrontRightTemp = 50.0
                };
                original.PilotName = PilotName;
                original.TyresTemperatures = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreTemperature.Received(1).UpdateFrontRight(
                    Arg.Is<TyresTemperatures>(c => c.FrontRightTemp == 50.0),
                    PilotName);
            }

            [Fact]
            public void GIVEN_telemetry_with_tyreTemperature_THEN_updateRearLeft()
            {
                // ARRANGE
                var original = new TelemetryModel();
                var tyres = new TyresTemperatures()
                {
                    RearLeftTemp = 50.0
                };
                original.PilotName = PilotName;
                original.TyresTemperatures = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreTemperature.Received(1).UpdateRearLeft(
                    Arg.Is<TyresTemperatures>(c => c.RearLeftTemp == 50.0),
                    PilotName);
            }

            [Fact]
            public void GIVEN_telemetry_with_tyreTemperature_THEN_updateRearRight()
            {
                // ARRANGE
                var original = new TelemetryModel();
                var tyres = new TyresTemperatures()
                {
                    RearRightTemp = 50.0
                };
                original.PilotName = PilotName;
                original.TyresTemperatures = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreTemperature.Received(1).UpdateRearRight(
                    Arg.Is<TyresTemperatures>(c => c.RearRightTemp == 50.0),
                    PilotName);
            }
        }

        public class WeatherConditionsTest
        {
            private TestContextPitwallTelemetryService _context;

            public WeatherConditionsTest()
            {
                _context = GetTargetTestContext();
            }

            [Fact]
            public void GIVEN_telemetry_with_nullAvgWetness_THEN_doNot_update()
            {
                // ARRANGE
                var original = new TelemetryModel();

                original.AvgWetness = null;
                original.PilotName = "Pilot1";

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.AvgWetnessRepository.Received(0)
                    .Update(Arg.Any<double?>(), Arg.Any<string>());
            }

            [Fact]
            public void GIVEN_telemetry_with_avgWetness_THEN_update_data_AND_set_pilotName()
            {
                // ARRANGE
                var original = new TelemetryModel();
                
                original.AvgWetness= 5.0;
                original.PilotName = "Pilot1";

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.AvgWetnessRepository.Received(1).Update(5.0, "Pilot1");
            }

            [Fact]
            public void GIVEN_telemetry_with_nullAirTemperature_THEN_doNot_update()
            {
                // ARRANGE
                var original = new TelemetryModel();

                original.AirTemperature = null;
                original.PilotName = "Pilot1";

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.AirTemperature.Received(0)
                    .Update(Arg.Any<double?>(), Arg.Any<string>());
            }

            [Fact]
            public void GIVEN_telemetry_with_airTemperatyre_THEN_update_data_AND_set_pilotName()
            {
                // ARRANGE
                var original = new TelemetryModel();

                original.AirTemperature = 5.0;
                original.PilotName = "Pilot1";

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.AirTemperature.Received(1).Update(5.0, "Pilot1");
            }

        }
    }
}
