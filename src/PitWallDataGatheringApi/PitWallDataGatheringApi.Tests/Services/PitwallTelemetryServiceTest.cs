using NSubstitute;
using PitWallDataGatheringApi.Models.Apis;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Services;

namespace PitWallDataGatheringApi.Tests.Services
{
    public class PitwallTelemetryServiceTest
    {
        private readonly ITyreWearRepository _tyreWearRepository;
        private readonly ILaptimeRepository _laptimeRepository;

        public PitwallTelemetryServiceTest() {
            _tyreWearRepository = Substitute.For<ITyreWearRepository>();

            _laptimeRepository = Substitute.For<ILaptimeRepository>();
        }    

        private PitwallTelemetryService GetTarget()
        {
            return new PitwallTelemetryService(_tyreWearRepository, _laptimeRepository);
        }

        [Fact]
        public void GIVEN_telemetry_isNull_THEN_should_not_updateTyres()
        {
            var target = GetTarget();

            // ACT
            target.Update(null);

            // ASSERT
            _tyreWearRepository.Received(0).UpdateFrontLeft(Arg.Any<Tyres>());
            _tyreWearRepository.Received(0).UpdateFrontRight(Arg.Any<Tyres>());
            _tyreWearRepository.Received(0).UpdateRearLeft(Arg.Any<Tyres>());
            _tyreWearRepository.Received(0).UpdateRearRight(Arg.Any<Tyres>());
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
        public void GIVEN_telemetry_with_tyresNull_THEN_should_not_updateTyres()
        {
            var original = new TelemetryModel();

            original.Tyres = null;

            // ACT
            var target = GetTarget();

            target.Update(original);

            // ASSERT
            _tyreWearRepository.Received(0).UpdateFrontLeft(Arg.Any<Tyres>());
            _tyreWearRepository.Received(0).UpdateFrontRight(Arg.Any<Tyres>());
            _tyreWearRepository.Received(0).UpdateRearLeft(Arg.Any<Tyres>());
            _tyreWearRepository.Received(0).UpdateRearRight(Arg.Any<Tyres>());
        }

        [Fact]
        public void GIVEN_telemetry_with_tyreWear_THEN_updateFrontLeft()
        {
            // ARRANGE
            var original = new TelemetryModel();

            original.Tyres.FrontLeftWear = 50.0;

            // ACT
            var target = GetTarget();

            target.Update(original);

            // ASSERT
            _tyreWearRepository.Received(1).UpdateFrontLeft(
                Arg.Is<Tyres>(c => c.FrontLeftWear == 50.0));
        }

        [Fact]
        public void GIVEN_telemetry_with_tyreWear_THEN_updateFrontRight()
        {
            // ARRANGE
            var original = new TelemetryModel();

            original.Tyres.FrontRightWear = 51.0;

            // ACT
            var target = GetTarget();

            target.Update(original);

            // ASSERT
            _tyreWearRepository.Received(1).UpdateFrontRight(
                Arg.Is<Tyres>(c => c.FrontRightWear == 51.0));
        }

        [Fact]
        public void GIVEN_telemetry_with_tyreWear_THEN_updateRearLeft()
        {
            // ARRANGE
            var original = new TelemetryModel();

            original.Tyres.ReartLeftWear = 52.0;

            // ACT
            var target = GetTarget();

            target.Update(original);

            // ASSERT
            _tyreWearRepository.Received(1).UpdateRearLeft(
                Arg.Is<Tyres>(c => c.ReartLeftWear == 52.0));
        }

        [Fact]
        public void GIVEN_telemetry_with_tyreWear_THEN_updateRearRight()
        {
            // ARRANGE
            var original = new TelemetryModel();

            original.Tyres.RearRightWear = 53.0;

            // ACT
            var target = GetTarget();

            target.Update(original);

            // ASSERT
            _tyreWearRepository.Received(1).UpdateRearRight(
                Arg.Is<Tyres>(c => c.RearRightWear == 53.0));
        }
    }
}
