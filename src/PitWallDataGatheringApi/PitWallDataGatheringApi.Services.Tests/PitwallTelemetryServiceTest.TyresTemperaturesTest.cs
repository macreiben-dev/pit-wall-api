using NSubstitute;
using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Models.Business;
using PitWallDataGatheringApi.Repositories;

namespace PitWallDataGatheringApi.Tests.Services
{
    public sealed partial class PitwallTelemetryServiceTest
    {
        public class TyresTemperaturesTest
        {
            private TestContextPitwallTelemetryService _context;

            private readonly PilotName PilotName = new PilotName("pilot01");
            private readonly CarName CarName = new CarName("SomeCar2");

            private TelemetryModel CreateModel()
            {
                return new TelemetryModel()
                {
                    PilotName = PilotName,
                    CarName = CarName
                };

            }
            public TyresTemperaturesTest()
            {
                _context = GetTargetTestContext();
            }

            [Fact]
            public void GIVEN_telemetry_with_TyresTemperatyres_isNull_THEN_should_not_updateTyres()
            {
                var original = CreateModel();

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreTemperature.Received(0).UpdateFrontLeft(Arg.Any<MetricData<double?>>());
                _context.TyreTemperature.Received(0).UpdateFrontRight(Arg.Any<MetricData<double?>>());
                _context.TyreTemperature.Received(0).UpdateRearLeft(Arg.Any<MetricData<double?>>());
                _context.TyreTemperature.Received(0).UpdateRearRight(Arg.Any<MetricData<double?>>());
            }

            // ------------------

            [Fact]
            public void GIVEN_tyreTemperatureFrontLeft_isNull_THEN_doNotUpdate()
            {
                // ARRANGE
                var original = CreateModel();

                var tyres = new TyresTemperatures()
                {
                    FrontLeftTemp = null
                };

                original.TyresTemperatures = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreTemperature.Received(0).UpdateFrontLeft(
                    Arg.Any<MetricData<double?>>());
            }

            [Fact]
            public void GIVEN_tyreTemperatureFrontLeft_isNotNull_THEN_update()
            {
                // ARRANGE
                var original = CreateModel();

                var tyres = new TyresTemperatures()
                {
                    FrontLeftTemp = 48.0
                };

                original.TyresTemperatures = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreTemperature.Received(1).UpdateFrontLeft(new MetricData<double?>(
                    48.0,
                    PilotName,
                    CarName));
            }

            // ------------------

            [Fact]
            public void GIVEN_tyreTemperatureFrontRight_isNull_THEN_doNotUpdate()
            {
                // ARRANGE
                var original = CreateModel();

                var tyres = new TyresTemperatures()
                {
                    FrontRightTemp = null
                };

                original.TyresTemperatures = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreTemperature.Received(0).UpdateFrontRight(
                    Arg.Any<MetricData<double?>>());
            }

            [Fact]
            public void GIVEN_tyreTemperatureFrontRight_isNotNull_THEN_update()
            {
                // ARRANGE
                var original = CreateModel();

                var tyres = new TyresTemperatures()
                {
                    FrontRightTemp = 49.0
                };

                original.TyresTemperatures = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreTemperature.Received(1).UpdateFrontRight(new MetricData<double?>(
                    49.0,
                    PilotName,
                    CarName));
            }

            // ------------------

            [Fact]
            public void GIVEN_tyreTemperatureRearLeft_isNull_THEN_doNotUpdate()
            {
                // ARRANGE
                var original = CreateModel();

                var tyres = new TyresTemperatures()
                {
                    RearLeftTemp = null
                };

                original.TyresTemperatures = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreTemperature.Received(0).UpdateRearLeft(
                    Arg.Any<MetricData<double?>>());
            }

            [Fact]
            public void GIVEN_tyreTemperatureReartLeft_isNotNull_THEN_update()
            {
                // ARRANGE
                var original = CreateModel();

                var tyres = new TyresTemperatures()
                {
                    RearLeftTemp = 50.0
                };

                original.TyresTemperatures = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreTemperature.Received(1).UpdateRearLeft(new MetricData<double?>(
                    50.0,
                    PilotName,
                    CarName));
            }

            // ------------------

            [Fact]
            public void GIVEN_tyreTemperatureRearRight_isNull_THEN_doNotUpdate()
            {
                // ARRANGE
                var original = CreateModel();

                var tyres = new TyresTemperatures()
                {
                    RearRightTemp = null
                };

                original.TyresTemperatures = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreTemperature.Received(0).UpdateRearRight(Arg.Any<MetricData<double?>>());
            }


            [Fact]
            public void GIVEN_tyreTemperatureReartRight_isNotNull_THEN_update()
            {
                // ARRANGE
                var original = CreateModel();

                var tyres = new TyresTemperatures()
                {
                    RearRightTemp = 51.0
                };

                original.TyresTemperatures = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreTemperature.Received(1).UpdateRearRight(new MetricData<double?>(
                    51.0,
                    PilotName,
                    CarName));
            }
        }
    }
}
