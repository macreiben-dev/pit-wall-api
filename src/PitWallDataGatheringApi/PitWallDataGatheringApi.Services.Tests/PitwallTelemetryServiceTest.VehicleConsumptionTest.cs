using NSubstitute;
using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Models.Business;
using PitWallDataGatheringApi.Repositories;

namespace PitWallDataGatheringApi.Tests.Services
{
    public sealed partial class PitwallTelemetryServiceTest
    {
        public class VehicleConsumptionTest
        {
            private const string PilotName = "Pilot1";
            private readonly CarName CarName = new CarName("SomeCarName1");

            private TestContextPitwallTelemetryService _context;

            public VehicleConsumptionTest()
            {
                _context = GetTargetTestContext();
            }

            [Fact]
            public void GIVEN_telemetry_with_null_ComputedLastLapConsumption_THEN_doNot_update()
            {
                // ARRANGE
                var original = new TelemetryModel();

                original.VehicleConsumption = new VehicleConsumption()
                {
                    ComputedLastLapConsumption = null,
                };

                // ACT & ASSERT
                EnsureRepoNotCalled(original, () => _context.LastLapConsumptionRepository);
            }

            [Fact]
            public void GIVEN_telemetry_with_ComputedLastLapConsumption_THEN_do_update()
            {
                // ARRANGE
                var original = new TelemetryModel();

                original.VehicleConsumption = new VehicleConsumption()
                {
                    ComputedLastLapConsumption = 13.2,
                };

                // ACT & ASSERT
                EnsureRepoCalledWithValue(original, 13.2, () => _context.LastLapConsumptionRepository);
            }

            // ------

            [Fact]
            public void GIVEN_telemetry_with_null_ComputedComputedLiterPerLaps_THEN_doNot_update()
            {
                // ARRANGE
                var original = new TelemetryModel();

                original.VehicleConsumption = new VehicleConsumption()
                {
                    ComputedLiterPerLaps = null,
                };

                // ACT & ASSERT
                EnsureRepoNotCalled(original, () => _context.LiterPerLapsRepository);
            }

            [Fact]
            public void GIVEN_telemetry_with_ComputedComputedLiterPerLaps_THEN_update()
            {
                // ARRANGE
                var original = new TelemetryModel();

                var originalValue = 13.3;

                original.VehicleConsumption = new VehicleConsumption()
                {
                    ComputedLiterPerLaps = originalValue,
                };

                EnsureRepoCalledWithValue(original, 13.3, () => _context.LiterPerLapsRepository);
            }

            // ------

            [Fact]
            public void GIVEN_telemetry_with_null_computedRemainingTime_THEN_doNot_update()
            {
                // ARRANGE
                var original = new TelemetryModel();

                original.VehicleConsumption = new VehicleConsumption()
                {
                    ComputedRemainingTime = null,
                };

                // ACT & ASSERT
                EnsureRepoNotCalled(original, () => _context.RemainingTimeRepository);
            }

            [Fact]
            public void GIVEN_telemetry_with_ComputedRemainingTime_THEN_update()
            {
                // ARRANGE
                var original = new TelemetryModel();

                var originalValue = 13.3;

                original.VehicleConsumption = new VehicleConsumption()
                {
                    ComputedRemainingTime = originalValue,
                };

                EnsureRepoCalledWithValue(original, 13.3, () => _context.RemainingTimeRepository);
            }

            // ------

            [Fact]
            public void GIVEN_telemetry_with_null_Fuel_THEN_doNot_update()
            {
                // ARRANGE
                var original = new TelemetryModel();

                original.VehicleConsumption = new VehicleConsumption()
                {
                    Fuel = null,
                };

                // ACT & ASSERT
                EnsureRepoNotCalled(original, () => _context.FuelRepository);
            }

            [Fact]
            public void GIVEN_telemetry_with_Fuel_THEN_update()
            {
                // ARRANGE
                var original = new TelemetryModel();

                var originalValue = 13.3;

                original.VehicleConsumption = new VehicleConsumption()
                {
                    Fuel = originalValue,
                };

                EnsureRepoCalledWithValue(original, 13.3, () => _context.FuelRepository);
            }

            // ------

            [Fact]
            public void GIVEN_telemetry_with_null_MaxFuel_THEN_doNot_update()
            {
                // ARRANGE
                var original = new TelemetryModel();

                original.VehicleConsumption = new VehicleConsumption()
                {
                    MaxFuel = null,
                };

                // ACT & ASSERT
                EnsureRepoNotCalled(original, () => _context.MaxFuelRepository);
            }

            [Fact]
            public void GIVEN_telemetry_with_MaxFuel_THEN_update()
            {
                // ARRANGE
                var original = new TelemetryModel();

                var originalValue = 13.3;

                original.VehicleConsumption = new VehicleConsumption()
                {
                    MaxFuel = originalValue,
                };

                EnsureRepoCalledWithValue(original, 13.3, () => _context.MaxFuelRepository);
            }

            // ================================================================================
            // ================================================================================
            // ================================================================================

            private void EnsureRepoNotCalled(
                TelemetryModel source,
                Func<IMetricRepository> selectRepository)
            {
                source.PilotName = PilotName;

                // ACT
                var target = _context.Target;

                target.Update(source);

                // ASSERT
                var metricRepo = selectRepository();

                metricRepo.Received(0)
                    .Update(Arg.Any<double?>(), Arg.Any<string>(), Arg.Any<CarName>());
            }

            private void EnsureRepoCalledWithValue(
                TelemetryModel source,
                double? expectedValue,
                Func<IMetricRepository> selectRepository)
            {
                source.PilotName = PilotName;
                source.CarName = CarName;

                // ACT
                var target = _context.Target;

                target.Update(source);

                // ASSERT
                var metricRepo = selectRepository();

                metricRepo.Received(1)
                    .Update(expectedValue, PilotName, CarName);
            }
        }
    }
}
