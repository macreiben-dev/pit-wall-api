using NSubstitute;
using PitWallDataGatheringApi.Controllers;
using PitWallDataGatheringApi.Models.Apis;
using PitWallDataGatheringApi.Services;

namespace PitWallDataGatheringApi.Tests.Controllers
{
    public class TelemetryControllerTest
    {
        private IPitwallTelemetryService _telemertryService;

        public TelemetryControllerTest() {
            _telemertryService = Substitute.For<IPitwallTelemetryService>();
        }

        [Fact]
        public void Should_post_metrics_without_failing()
        {
            var target = new TelemetryController(_telemertryService);

            var original = new TelemetryModel();

            original.PilotName = "Pilot1";
            original.LaptimeSeconds = 122.500;

            target.Post(original);

            _telemertryService.Received(1).Update(original);
        }
    }
}
