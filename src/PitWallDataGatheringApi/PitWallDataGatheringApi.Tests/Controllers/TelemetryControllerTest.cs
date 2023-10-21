using NSubstitute;
using PitWallDataGatheringApi.Controllers;
using PitWallDataGatheringApi.Services;

using IBusinessTelemetryModel = PitWallDataGatheringApi.Models.Business.ITelemetryModel;
using ApiTelemetryModel = PitWallDataGatheringApi.Models.Apis.TelemetryModel;
using BusinessTelemetryModel = PitWallDataGatheringApi.Models.Business.TelemetryModel;
using NFluent;
using AutoMapper;
using PitWallDataGatheringApi.Repositories;
using Microsoft.AspNetCore.Mvc;

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
            _simerKeyReposity.Key.Returns("OkKey");

            // a fake mapper with nsubstitute
            _mapper.Map(Arg.Any<ApiTelemetryModel>())
                .Returns(c => new BusinessTelemetryModel()
                {
                    PilotName = c.Arg<ApiTelemetryModel>().PilotName
                });

            // ACT
            TelemetryController target = GetTarget();

            target.Post(original);

            _telemertryService
                .Received(1)
                .Update(Arg.Is<IBusinessTelemetryModel>(c => c.PilotName == "Pilot1"));
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
    }
}
