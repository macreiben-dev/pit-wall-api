﻿using NSubstitute;
using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Models.Business;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Repositories.Prometheus;
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
        private readonly IAvgWetnessRepository _avgWetness;
        private readonly IAirTemperatureRepository _airTemperature;
        private readonly ITrackTemperatureRepository _trackTemperature;

        private const string PilotName = "Pilot01";

        public PitwallTelemetryServiceTest()
        {
            _tyreWearRepository = Substitute.For<ITyreWearRepository>();

            _laptimeRepository = Substitute.For<ILaptimeRepository>();

            _tyreTemperature = Substitute.For<ITyresTemperaturesRepository>();
            
            _avgWetness = Substitute.For<IAvgWetnessRepository>();

            _airTemperature = Substitute.For<IAirTemperatureRepository>();

            _trackTemperature = Substitute.For<ITrackTemperatureRepository>();
        }

        private PitwallTelemetryService GetTarget()
        {
            return new PitwallTelemetryService(
                _tyreWearRepository,
                _laptimeRepository,
                _tyreTemperature,
                _avgWetness,
                Substitute.For<IAirTemperatureRepository>(), 
                _trackTemperature);
        }

        public static TestContextPitwallTelemetryService GetTargetTestContext()
        {
            var tyreWearRepository = Substitute.For<ITyreWearRepository>();

            var laptimeRepository = Substitute.For<ILaptimeRepository>();

            var tyreTemperature = Substitute.For<ITyresTemperaturesRepository>();

            var avgWetnessRepository = Substitute.For<IAvgWetnessRepository>();

            var airTemperature = Substitute.For<IAirTemperatureRepository>();

            var trackTemperature = Substitute.For<ITrackTemperatureRepository>();

            var target = new PitwallTelemetryService(
                tyreWearRepository,
                laptimeRepository,
                tyreTemperature,
                avgWetnessRepository,
                airTemperature,
                trackTemperature);

            return new TestContextPitwallTelemetryService(
                target,
                tyreWearRepository,
                tyreTemperature,
                laptimeRepository,
                avgWetnessRepository,
                airTemperature, 
                trackTemperature);
        }

        [Fact]
        public void GIVEN_telemetry_isNull_THEN_should_not_updateTyres()
        {
            var target = GetTarget();

            // ACT
            target.Update(null);

            // ASSERT
            _tyreWearRepository.Received(0).UpdateFrontLeft(PilotName, Arg.Any<double?>());
            _tyreWearRepository.Received(0).UpdateFrontRight(PilotName, Arg.Any<double?>());
            _tyreWearRepository.Received(0).UpdateRearLeft(PilotName, Arg.Any<double?>());
            _tyreWearRepository.Received(0).UpdateRearRight(PilotName, Arg.Any<double?>());
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


        [Fact]
        public void GIVEN_telemetry_isNull_THEN_should_not_update_avgWetness()
        {
            var target = GetTarget();

            // ACT
            target.Update(null);

            // ASSERT
            _avgWetness.Received(0).Update(Arg.Any<double?>(), Arg.Any<string>());
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
        public void GIVEN_telemetry_isNull_THEN_should_not_update_trackTemperature()
        {
            var target = GetTarget();

            // ACT
            target.Update(null);

            // ASSERT
            _trackTemperature.Received(0).Update(Arg.Any<double?>(), Arg.Any<string>(), new CarName(null));
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

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreWearRepository.Received(0).UpdateFrontLeft(PilotName, Arg.Any<double?>());
                _context.TyreWearRepository.Received(0).UpdateFrontRight(PilotName, Arg.Any<double?>());
                _context.TyreWearRepository.Received(0).UpdateRearLeft(PilotName, Arg.Any<double?>());
                _context.TyreWearRepository.Received(0).UpdateRearRight(PilotName, Arg.Any<double?>());
            }

            [Fact]
            public void GIVEN_telemetry_with_tyreWear_THEN_updateFrontLeft()
            {
                // ARRANGE
                var original = new TelemetryModel();
                original.PilotName = PilotName;

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
                    PilotName, 
                    50.0);
            }

            [Fact]
            public void GIVEN_telemetry_with_tyreWear_THEN_updateFrontRight()
            {
                // ARRANGE
                var original = new TelemetryModel();
                original.PilotName = PilotName;

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
                    PilotName,
                    51.0);
            }

            [Fact]
            public void GIVEN_telemetry_with_tyreWear_THEN_updateRearLeft()
            {
                // ARRANGE
                var original = new TelemetryModel();
                original.PilotName = PilotName;

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
                    PilotName,
                    52.0);
            }

            [Fact]
            public void GIVEN_telemetry_with_tyreWear_THEN_updateRearRight()
            {
                // ARRANGE
                var original = new TelemetryModel();
                original.PilotName = PilotName;

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
                    PilotName,
                    53.0);
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

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreTemperature.Received(0).UpdateFrontLeft(PilotName, Arg.Any<double?>());
                _context.TyreTemperature.Received(0).UpdateFrontRight(PilotName, Arg.Any<double?>());
                _context.TyreTemperature.Received(0).UpdateRearLeft(PilotName, Arg.Any<double?>());
                _context.TyreTemperature.Received(0).UpdateRearRight(PilotName, Arg.Any<double?>());
            }

            [Fact]
            public void GIVEN_telemetry_with_tyreTemperature_THEN_updateFrontLeft()
            {
                // ARRANGE
                var original = new TelemetryModel();
                var tyres = new TyresTemperatures()
                {
                    FrontLeftTemp = 48.0
                };

                original.TyresTemperatures = tyres;
                original.PilotName = PilotName;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreTemperature.Received(1).UpdateFrontLeft(
                    PilotName,
                    48.0);
            }

            [Fact]
            public void GIVEN_telemetry_with_tyreTemperature_THEN_updateFrontRight()
            {
                // ARRANGE
                var original = new TelemetryModel();
                var tyres = new TyresTemperatures()
                {
                    FrontRightTemp = 49.0
                };
                original.PilotName = PilotName;
                original.TyresTemperatures = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreTemperature.Received(1).UpdateFrontRight(
                    PilotName,
                    49.0);
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
                    PilotName, 
                    50.0);
            }

            [Fact]
            public void GIVEN_telemetry_with_tyreTemperature_THEN_updateRearRight()
            {
                // ARRANGE
                var original = new TelemetryModel();
                var tyres = new TyresTemperatures()
                {
                    RearRightTemp = 51.0
                };
                original.PilotName = PilotName;
                original.TyresTemperatures = tyres;

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TyreTemperature.Received(1).UpdateRearRight(
                    PilotName,
                    51.0);
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

                original.AvgWetness = 5.0;
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
                    .Update(Arg.Any<double?>(), Arg.Any<string>(), Arg.Any<CarName>());
            }

            [Fact]
            public void GIVEN_telemetry_with_airTemperature_THEN_update_data_AND_set_pilotName()
            {
                // ARRANGE
                var original = new TelemetryModel();

                original.AirTemperature = 5.0;
                original.PilotName = "Pilot1";

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.AirTemperature.Received(1).Update(5.0, "Pilot1", CarName.Null());
            }

            [Fact]
            public void GIVEN_telemetry_with_nullTrackTemperature_THEN_doNot_update()
            {
                // ARRANGE
                var original = new TelemetryModel();

                original.TrackTemperature = null;
                original.PilotName = "Pilot1";

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TrackTemperature.Received(0)
                    .Update(Arg.Any<double?>(), Arg.Any<string>(), Arg.Any<CarName>());
            }

            [Fact]
            public void GIVEN_telemetry_with_trackTemperature_THEN_update_data_AND_set_pilotName()
            {
                // ARRANGE
                var original = new TelemetryModel();

                original.TrackTemperature = 13.3;
                original.PilotName = "Pilot1";

                // ACT
                var target = _context.Target;

                target.Update(original);

                // ASSERT
                _context.TrackTemperature.Received(1)
                    .Update(13.3, "Pilot1", Arg.Any<CarName>());
            }

        }
    }
}