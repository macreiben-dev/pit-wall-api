using NSubstitute;
using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Models.Business;
using PitWallDataGatheringApi.Repositories;

namespace PitWallDataGatheringApi.Tests.Services
{
    public sealed partial class PitwallTelemetryServiceTest
    {
        public class TyreWearTest
        {
            private TestContextPitwallTelemetryService _context;
            private readonly CarName CarName = new CarName("SomeCarName1");
            private readonly PilotName PilotName = new PilotName("SomePilotName");

            public TyreWearTest()
            {
                _context = GetTargetTestContext();
            }

            [Fact]
            public void GIVEN_telemetry_with_TyresWear_isNull_THEN_should_not_updateTyres()
            {
                var original = new TelemetryModel();

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreWearRepository.Received(0).UpdateFrontLeft(Arg.Any<MetricData<double?>>());
                _context.TyreWearRepository.Received(0).UpdateFrontRight(Arg.Any<MetricData<double?>>());
                _context.TyreWearRepository.Received(0).UpdateRearLeft(Arg.Any<MetricData<double?>>());
                _context.TyreWearRepository.Received(0).UpdateRearRight(Arg.Any<MetricData<double?>>());
            }

            // ------------------

            [Fact]
            public void GIVEN_tyreWearFrontLeft_isNull_THEN_doNot_update()
            {
                // ARRANGE
                TelemetryModel original = CreateModel();

                var tyres = new TyresWear()
                {
                    FrontLeftWear = null
                };

                original.TyresWear = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreWearRepository.Received(0).UpdateFrontLeft(Arg.Any<MetricData<double?>>());
            }

            [Fact]
            public void GIVEN_tyreWearFrontLeft_isNotNull_THEN_update()
            {
                // ARRANGE
                var original = CreateModel();

                var tyres = new TyresWear()
                {
                    FrontLeftWear = 50.0
                };

                original.TyresWear = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreWearRepository.Received(1).UpdateFrontLeft(new MetricData<double?>(
                    50.0,
                    PilotName,
                    CarName));
            }

            // ------------------

            [Fact]
            public void GIVEN_tyreWearFrontRight_isNull_THEN_doNot_update()
            {
                // ARRANGE
                var original = CreateModel();

                var tyres = new TyresWear()
                {
                    FrontRightWear = null
                };

                original.TyresWear = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreWearRepository.Received(0).UpdateFrontRight(Arg.Any<MetricData<double?>>());
            }

            [Fact]
            public void GIVEN_tyreWearFrontRight_isNotNull_THEN_update()
            {
                // ARRANGE
                var original = CreateModel();

                var tyres = new TyresWear()
                {
                    FrontRightWear = 51.0
                };

                original.TyresWear = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreWearRepository.Received(1).UpdateFrontRight(new MetricData<double?>(
                    51.0,
                    PilotName,
                    CarName));
            }

            // ------------------

            [Fact]
            public void GIVEN_tyreWearRearLeft_isNull_THEN_doNot_update()
            {
                // ARRANGE
                var original = CreateModel();

                var tyres = new TyresWear()
                {
                    ReartLeftWear = null
                };

                original.TyresWear = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreWearRepository.Received(0).UpdateRearLeft(Arg.Any<MetricData<double?>>());
            }

            [Fact]
            public void GIVEN_tyreWearRearLeft_isNotNull_THEN_update()
            {
                // ARRANGE
                var original = CreateModel();

                var tyres = new TyresWear()
                {
                    ReartLeftWear = 52.0
                };

                original.TyresWear = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreWearRepository.Received(1).UpdateRearLeft(new MetricData<double?>(
                    52.0,
                    PilotName,
                    CarName));
            }

            // ------------------

            [Fact]
            public void GIVEN_tyreWearRearRight_isNull_THEN_doNot_update()
            {
                // ARRANGE
                var original = CreateModel();

                var tyres = new TyresWear()
                {
                    RearRightWear = null
                };

                original.TyresWear = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreWearRepository.Received(0).UpdateRearRight(Arg.Any<MetricData<double?>>());
            }

            [Fact]
            public void GIVEN_tyreWearRearRight_isNotNull_THEN_update()
            {
                // ARRANGE
                var original = CreateModel();

                var tyres = new TyresWear()
                {
                    RearRightWear = 53.0
                };

                original.TyresWear = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreWearRepository.Received(1).UpdateRearRight(new MetricData<double?>(
                    53.0,
                    PilotName,
                    CarName));
            }

            private TelemetryModel CreateModel()
            {
                var original = new TelemetryModel();
                original.PilotName = PilotName;
                original.CarName = CarName;
                return original;
            }
        }
    }
}
