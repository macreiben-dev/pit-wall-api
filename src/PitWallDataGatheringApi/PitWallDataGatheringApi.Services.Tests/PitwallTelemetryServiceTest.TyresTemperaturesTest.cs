﻿using NSubstitute;
using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Models.Business;

namespace PitWallDataGatheringApi.Tests.Services
{
    public sealed partial class PitwallTelemetryServiceTest
    {
        public class TyresTemperaturesTest
        {
            private TestContextPitwallTelemetryService _context;

            private const string PilotName = "pilot01";
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
                _context.TyreTemperature.Received(0).UpdateFrontLeft(Arg.Any<double?>(), PilotName, Arg.Any<CarName>());
                _context.TyreTemperature.Received(0).UpdateFrontRight(Arg.Any<double?>(), PilotName, Arg.Any<CarName>());
                _context.TyreTemperature.Received(0).UpdateRearLeft(PilotName, Arg.Any<double?>(), Arg.Any<CarName>());
                _context.TyreTemperature.Received(0).UpdateRearRight(PilotName, Arg.Any<double?>(), Arg.Any<CarName>());
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
                    Arg.Any<double?>(),
                    Arg.Any<string>(),
                    Arg.Any<CarName>());
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
                _context.TyreTemperature.Received(1).UpdateFrontLeft(
                    48.0,
                    PilotName,
                    CarName);
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
                    Arg.Any<double?>(),
                    Arg.Any<string>(),
                    Arg.Any<CarName>());
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
                _context.TyreTemperature.Received(1).UpdateFrontRight(
                    49.0,
                    PilotName,
                    CarName);
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
                    Arg.Any<string>(),
                    Arg.Any<double?>(),
                    Arg.Any<CarName>());
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
                _context.TyreTemperature.Received(1).UpdateRearLeft(
                    PilotName,
                    50.0,
                    CarName);
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
                _context.TyreTemperature.Received(0).UpdateRearRight(
                    Arg.Any<string>(),
                    Arg.Any<double?>(),
                    Arg.Any<CarName>());
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
                _context.TyreTemperature.Received(1).UpdateRearRight(
                    PilotName,
                    51.0,
                    CarName);
            }
        }
    }
}
