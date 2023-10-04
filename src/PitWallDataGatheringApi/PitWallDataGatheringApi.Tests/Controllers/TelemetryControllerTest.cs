using NSubstitute;
using PitWallDataGatheringApi.Controllers;
using PitWallDataGatheringApi.Services;

using IBusinessTelemetryModel = PitWallDataGatheringApi.Models.Business.ITelemetryModel;
using ApiTelemetryModel = PitWallDataGatheringApi.Models.Apis.TelemetryModel;
using BusinessTelemetryModel = PitWallDataGatheringApi.Models.Business.TelemetryModel;

namespace PitWallDataGatheringApi.Tests.Controllers
{
    public class TelemetryControllerTest
    {
        private IPitwallTelemetryService _telemertryService;
        private ITelemetryModelMapper _mapper;

        public TelemetryControllerTest()
        {
            _telemertryService = Substitute.For<IPitwallTelemetryService>();
            _mapper = Substitute.For<ITelemetryModelMapper>();
        }

        [Fact]
        public void Should_post_metrics_without_failing()
        {
            // ARRANGE
            var original = new ApiTelemetryModel();

            original.PilotName = "Pilot1";

            // a fake mapper with nsubstitute
            _mapper.Map(Arg.Any<ApiTelemetryModel>())
                .Returns(c => new BusinessTelemetryModel()
                {
                    PilotName = c.Arg<ApiTelemetryModel>().PilotName
                });

            // ACT
            var target = new TelemetryController(_telemertryService, _mapper);

            target.Post(original);

            _telemertryService
                .Received(1)
                .Update(Arg.Is<IBusinessTelemetryModel>(c => c.PilotName == "Pilot1"));
        }
    }
}
