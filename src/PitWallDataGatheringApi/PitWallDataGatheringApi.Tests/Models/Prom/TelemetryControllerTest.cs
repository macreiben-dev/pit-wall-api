using NFluent;
using PitWallDataGatheringApi.Controllers;
using PitWallDataGatheringApi.Models.Apis;

namespace PitWallDataGatheringApi.Tests.Models.Prom
{
    public class TelemetryControllerTest
    {
        [Fact]
        public void Should_post_metrics_without_failing()
        {
            var target = new TelemetryController();

            var original = new TelemetryModel();

            original.PilotName = "Pilot1";
            original.LaptimeSeconds = 122.500;

            Check.ThatCode(() => target.Post(original)).DoesNotThrow();
        }
    }
}
