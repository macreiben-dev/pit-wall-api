using NSubstitute;
using PitWallDataGatheringApi.Controllers;
using PitWallDataGatheringApi.Services;

using IBusinessTelemetryModel = PitWallDataGatheringApi.Models.Business.ITelemetryModel;
using ApiTelemetryModel = PitWallDataGatheringApi.Models.Apis.v1.TelemetryModel;
using BusinessTelemetryModel = PitWallDataGatheringApi.Models.Business.TelemetryModel;
using NFluent;
using PitWallDataGatheringApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using PitWallDataGatheringApi.Controllers.v1;

namespace PitWallDataGatheringApi.Tests.Controllers
{
    public class TelemetryControllerTest
    {
        private IPitwallTelemetryService _telemertryService;
        private ITelemetryModelMapper _mapper;
        private ISimerKeyRepository _simerKeyReposity;

        public TelemetryControllerTest()
        {
            _telemertryService = Substitute.For<IPitwallTelemetryService>();
            
            _mapper = Substitute.For<ITelemetryModelMapper>();

            _simerKeyReposity = Substitute.For<ISimerKeyRepository>();
        }

        private TelemetryController GetTarget()
        {
            return new TelemetryController(
                _telemertryService, 
                _mapper, 
                _simerKeyReposity);
        }

        [Fact]
        public void Should_post_metrics_without_failing()
        {
            // ARRANGE
            var original = new ApiTelemetryModel();

            original.PilotName = "Pilot1";
            original.SimerKey = "OkKey";
            original.CarName = "SomeCarName";

            _simerKeyReposity.Key.Returns("OkKey");

            // a fake mapper with nsubstitute
            _mapper.Map(Arg.Any<ApiTelemetryModel>())
                .Returns(c => new BusinessTelemetryModel()
                {
                    PilotName = new Models.PilotName(c.Arg<ApiTelemetryModel>().PilotName)
                });

            // ACT
            TelemetryController target = GetTarget();

            target.Post(original);

            _telemertryService
                .Received(1)
                .Update(Arg.Is<IBusinessTelemetryModel>(c => c.PilotName.ToString() == "Pilot1"));
        }

        [Fact]
        public void GIVEN_simerKey_received_equals_simerKey_configured_THEN_return_denied()
        {
            _simerKeyReposity.Key.Returns("other");

            var original = new ApiTelemetryModel();

            original.SimerKey = "Key1";

            var target = GetTarget();

            var intermediary = target.Post(original);

            var actual = (StatusCodeResult)intermediary;

            Check.That(actual.StatusCode).IsEqualTo(401);
        }

        [Fact]
        public void GIVEN_noPilotName_THEN_return_badRequest_AND_original_payload()
        {
            _simerKeyReposity.Key.Returns("Key1");

            var original = new ApiTelemetryModel();

            original.SimerKey = "Key1";
            original.PilotName = null;

            var target = GetTarget();

            var intermediary = target.Post(original);

            var actual = (BadRequestObjectResult)intermediary;
            var payload = (ErrorMessages)actual.Value;

            Check.That(actual.StatusCode).IsEqualTo(400);
            
            Check.That(payload.Errors.FirstOrDefault()).IsEqualTo("Pilot name is mandatory.");

            Check.That(payload.Source.PilotName).IsNull();
            Check.That(payload.Source.SimerKey).IsEqualTo("Key1");
        }

        [Fact]
        public void GIVEN_noCarName_THEN_return_badRequest_AND_original_payload()
        {
            _simerKeyReposity.Key.Returns("Key1");

            var original = new ApiTelemetryModel();

            original.SimerKey = "Key1";
            original.PilotName = "somePilot";
            original.CarName = null;

            var target = GetTarget();

            var intermediary = target.Post(original);

            var actual = (BadRequestObjectResult)intermediary;
            var payload = (ErrorMessages)actual.Value;

            Check.That(actual.StatusCode).IsEqualTo(400);

            Check.That(payload.Errors.FirstOrDefault()).IsEqualTo("Car name is mandatory.");

            Check.That(payload.Source.CarName).IsNull();
            Check.That(payload.Source.SimerKey).IsEqualTo("Key1");
        }
    }
}
