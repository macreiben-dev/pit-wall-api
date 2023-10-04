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

            var target = GetTarget();

            // ACT
            target.Update(original);

            // ASSERT
            _tyreWearRepository.Received(0).UpdateFrontLeft(Arg.Any<Tyres>());
            _tyreWearRepository.Received(0).UpdateFrontRight(Arg.Any<Tyres>());
            _tyreWearRepository.Received(0).UpdateRearLeft(Arg.Any<Tyres>());
            _tyreWearRepository.Received(0).UpdateRearRight(Arg.Any<Tyres>());
        }
    }
}
