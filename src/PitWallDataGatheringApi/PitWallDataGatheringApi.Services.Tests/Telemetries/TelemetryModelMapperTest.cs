﻿using ApiTelemetryModel = PitWallDataGatheringApi.Models.Apis.v1.TelemetryModel;
using ApiVehicleConsumption = PitWallDataGatheringApi.Models.Apis.v1.VehicleConsumption;
using NFluent;
using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Services.Telemetries;

namespace PitWallDataGatheringApi.Tests.Services
{
    public partial class TelemetryModelMapperTest
    {
        public class VehicleConsumption
        {
            private TelemetryModelMapper GetTarget()
            {
                return new TelemetryModelMapper();
            }

            [Fact]
            public void GIVEN_sourceVehicleConsumption_isNull_THEN_mapEmpty()
            {
                ApiTelemetryModel source = new ApiTelemetryModel();

                source.VehicleConsumption = null;

                var target = GetTarget();

                var actual = target.Map(source);

                Check.That(actual.VehicleConsumption).IsNotNull();

                Check.That(actual.VehicleConsumption.Fuel).IsNull();
                Check.That(actual.VehicleConsumption.MaxFuel).IsNull();
                Check.That(actual.VehicleConsumption.ComputedLiterPerLaps).IsNull();
                Check.That(actual.VehicleConsumption.ComputedRemainingTime).IsNull();
                Check.That(actual.VehicleConsumption.ComputedRemainingLaps).IsNull();
                Check.That(actual.VehicleConsumption.ComputedLastLapConsumption).IsNull();
            }

            [Fact]
            public void GIVEN_vehicleConsumption_is_notNull_THEN_mapFuel_only()
            {
                ApiTelemetryModel source = new ApiTelemetryModel();

                source.VehicleConsumption = new ApiVehicleConsumption();

                double expected = 50.0;

                source.VehicleConsumption.Fuel = expected;

                var target = GetTarget();

                var actual = target.Map(source);

                Check.That(actual.VehicleConsumption.Fuel).IsEqualTo(expected);
                Check.That(actual.VehicleConsumption.MaxFuel).IsNull();
                Check.That(actual.VehicleConsumption.ComputedLiterPerLaps).IsNull();
                Check.That(actual.VehicleConsumption.ComputedRemainingLaps).IsNull();
                Check.That(actual.VehicleConsumption.ComputedRemainingTime).IsNull();
                Check.That(actual.VehicleConsumption.ComputedLastLapConsumption).IsNull();
            }

            [Fact]
            public void GIVEN_vehicleConsumption_is_notNull_THEN_mapMaxFuel_only()
            {
                ApiTelemetryModel source = new ApiTelemetryModel();

                source.VehicleConsumption = new ApiVehicleConsumption();

                double expected = 50.0;

                source.VehicleConsumption.MaxFuel = expected;

                var target = GetTarget();

                var actual = target.Map(source);

                Check.That(actual.VehicleConsumption.Fuel).IsNull();
                Check.That(actual.VehicleConsumption.MaxFuel).IsEqualTo(expected);
                Check.That(actual.VehicleConsumption.ComputedLiterPerLaps).IsNull();
                Check.That(actual.VehicleConsumption.ComputedRemainingLaps).IsNull();
                Check.That(actual.VehicleConsumption.ComputedRemainingTime).IsNull();
                Check.That(actual.VehicleConsumption.ComputedLastLapConsumption).IsNull();
            }

            [Fact]
            public void GIVEN_vehicleConsumption_is_notNull_THEN_mapComputedLiterPerLaps_only()
            {
                ApiTelemetryModel source = new ApiTelemetryModel();

                source.VehicleConsumption = new ApiVehicleConsumption();

                double expected = 50.0;

                source.VehicleConsumption.ComputedLiterPerLaps = expected;

                var target = GetTarget();

                var actual = target.Map(source);

                Check.That(actual.VehicleConsumption.Fuel).IsNull();
                Check.That(actual.VehicleConsumption.MaxFuel).IsNull();
                Check.That(actual.VehicleConsumption.ComputedLiterPerLaps).IsEqualTo(expected);
                Check.That(actual.VehicleConsumption.ComputedRemainingLaps).IsNull();
                Check.That(actual.VehicleConsumption.ComputedRemainingTime).IsNull();
                Check.That(actual.VehicleConsumption.ComputedLastLapConsumption).IsNull();
            }

            [Fact]
            public void GIVEN_vehicleConsumption_is_notNull_THEN_mapComputedRemainingLaps_only()
            {
                ApiTelemetryModel source = new ApiTelemetryModel();

                source.VehicleConsumption = new ApiVehicleConsumption();

                double expected = 50.0;

                source.VehicleConsumption.ComputedRemainingLaps = expected;

                var target = GetTarget();

                var actual = target.Map(source);

                Check.That(actual.VehicleConsumption.Fuel).IsNull();
                Check.That(actual.VehicleConsumption.MaxFuel).IsNull();
                Check.That(actual.VehicleConsumption.ComputedLiterPerLaps).IsNull();
                Check.That(actual.VehicleConsumption.ComputedRemainingLaps).IsEqualTo(expected);
                Check.That(actual.VehicleConsumption.ComputedRemainingTime).IsNull();
                Check.That(actual.VehicleConsumption.ComputedLastLapConsumption).IsNull();
            }

            [Fact]
            public void GIVEN_vehicleConsumption_is_notNull_THEN_mapComputedRemainingTime_only()
            {
                ApiTelemetryModel source = new ApiTelemetryModel();

                source.VehicleConsumption = new ApiVehicleConsumption();

                double expected = 50.0;

                source.VehicleConsumption.ComputedRemainingTime = expected;

                var target = GetTarget();

                var actual = target.Map(source);

                Check.That(actual.VehicleConsumption.Fuel).IsNull();
                Check.That(actual.VehicleConsumption.MaxFuel).IsNull();
                Check.That(actual.VehicleConsumption.ComputedLiterPerLaps).IsNull();
                Check.That(actual.VehicleConsumption.ComputedRemainingLaps).IsNull();
                Check.That(actual.VehicleConsumption.ComputedRemainingTime).IsEqualTo(expected);
                Check.That(actual.VehicleConsumption.ComputedLastLapConsumption).IsNull();
            }

            [Fact]
            public void GIVEN_vehicleConsumption_is_notNull_THEN_mapLastLapConsumption_only()
            {
                ApiTelemetryModel source = new ApiTelemetryModel();

                source.VehicleConsumption = new ApiVehicleConsumption();

                double expected = 50.0;

                source.VehicleConsumption.ComputedLastLapConsumption = expected;

                var target = GetTarget();

                var actual = target.Map(source);

                Check.That(actual.VehicleConsumption.Fuel).IsNull();
                Check.That(actual.VehicleConsumption.MaxFuel).IsNull();
                Check.That(actual.VehicleConsumption.ComputedLiterPerLaps).IsNull();
                Check.That(actual.VehicleConsumption.ComputedRemainingLaps).IsNull();
                Check.That(actual.VehicleConsumption.ComputedRemainingTime).IsNull();
                Check.That(actual.VehicleConsumption.ComputedLastLapConsumption).IsEqualTo(expected);
            }

            [Fact]
            public void GIVEN_carName_is_notNull_THEN_mapCarName()
            {
                ApiTelemetryModel source = new ApiTelemetryModel();

                source.CarName = "SomeCarName";

                var target = GetTarget();

                var actual = target.Map(source);

                Check.That(actual.CarName).IsEqualTo(new CarName("SomeCarName"));
            }

            [Fact]
            public void GIVEN_pilotName_is_notNull_THEN_maPilotName()
            {
                ApiTelemetryModel source = new ApiTelemetryModel();

                source.PilotName = "SomeName";

                var target = GetTarget();

                var actual = target.Map(source);

                Check.That(actual.PilotName).IsEqualTo(new PilotName("SomeName"));
            }

        }
    }
}
