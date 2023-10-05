using NSubstitute;
using PitWallDataGatheringApi.Models.Business;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Repositories.Tyres;
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
                _tyreTemperature);
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


        #region tyre wear

        [Fact]
        public void GIVEN_telemetry_with_TyresWear_isNull_THEN_should_not_updateTyres()
        {
            var original = new TelemetryModel();

            original.TyresWear = null;

            // ACT
            var target = GetTarget();

            target.Update(original);

            // ASSERT
            _tyreWearRepository.Received(0).UpdateFrontLeft(Arg.Any<ITyresWear>());
            _tyreWearRepository.Received(0).UpdateFrontRight(Arg.Any<ITyresWear>());
            _tyreWearRepository.Received(0).UpdateRearLeft(Arg.Any<ITyresWear>());
            _tyreWearRepository.Received(0).UpdateRearRight(Arg.Any<ITyresWear>());
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
            var target = GetTarget();

            target.Update(original);

            // ASSERT
            _tyreWearRepository.Received(1).UpdateFrontLeft(
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
            var target = GetTarget();

            target.Update(original);

            // ASSERT
            _tyreWearRepository.Received(1).UpdateFrontRight(
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
            var target = GetTarget();

            target.Update(original);

            // ASSERT
            _tyreWearRepository.Received(1).UpdateRearLeft(
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
            var target = GetTarget();

            target.Update(original);

            // ASSERT
            _tyreWearRepository.Received(1).UpdateRearRight(
                Arg.Is<TyresWear>(c => c.RearRightWear == 53.0));
        }

        #endregion tyre wear

        #region tyre temperature

        [Fact]
        public void GIVEN_telemetry_with_TyresTemperatyres_isNull_THEN_should_not_updateTyres()
        {
            var original = new TelemetryModel();

            original.TyresWear = null;

            // ACT
            var target = GetTarget();

            target.Update(original);

            // ASSERT
            _tyreTemperature.Received(0).UpdateFrontLeft(Arg.Any<ITyresTemperatures>());
            _tyreTemperature.Received(0).UpdateFrontRight(Arg.Any<ITyresTemperatures>());
            _tyreTemperature.Received(0).UpdateRearLeft(Arg.Any<ITyresTemperatures>());
            _tyreTemperature.Received(0).UpdateRearRight(Arg.Any<ITyresTemperatures>());
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

            // ACT
            var target = GetTarget();

            target.Update(original);

            // ASSERT
            _tyreTemperature.Received(1).UpdateFrontLeft(
                Arg.Is<TyresTemperatures>(c => c.FrontLeftTemp == 50.0));
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

            original.TyresTemperatures = tyres;

            // ACT
            var target = GetTarget();

            target.Update(original);

            // ASSERT
            _tyreTemperature.Received(1).UpdateFrontRight(
                Arg.Is<TyresTemperatures>(c => c.FrontRightTemp == 50.0));
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

            original.TyresTemperatures = tyres;

            // ACT
            var target = GetTarget();

            target.Update(original);

            // ASSERT
            _tyreTemperature.Received(1).UpdateRearLeft(
                Arg.Is<TyresTemperatures>(c => c.RearLeftTemp == 50.0));
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

            original.TyresTemperatures = tyres;

            // ACT
            var target = GetTarget();

            target.Update(original);

            // ASSERT
            _tyreTemperature.Received(1).UpdateRearRight(
                Arg.Is<TyresTemperatures>(c => c.RearRightTemp == 50.0));
        }

        #endregion tyre temperature
    }
}
