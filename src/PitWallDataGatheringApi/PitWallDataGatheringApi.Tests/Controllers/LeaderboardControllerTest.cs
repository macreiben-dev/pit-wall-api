using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using PitWallDataGatheringApi.Controllers;
using PitWallDataGatheringApi.Controllers.v1;
using PitWallDataGatheringApi.Models.Apis.v1;
using PitWallDataGatheringApi.Models.Apis.v1.Leaderboards;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Services;
using PitWallDataGatheringApi.Services.Leaderboards;
using ApiModel = PitWallDataGatheringApi.Models.Apis.v1.Leaderboards.LeaderboardModel;
using BusinessModel = PitWallDataGatheringApi.Models.Business.Leaderboards.LeaderboardModel;
using IBusinessModel = PitWallDataGatheringApi.Models.Business.Leaderboards.ILeaderboardModel;


namespace PitWallDataGatheringApi.Tests.Controllers
{
    public class LeaderboardControllerTest
    {
        private readonly ILeaderboardModelMapper _mapper;
        private readonly ISimerKeyRepository _simerKeyReposity;
        private readonly ILeaderBoardService _service;
        private readonly IAuthenticatePayloadService _authenticatePayload;

        public LeaderboardControllerTest()
        {
            _mapper = Substitute.For<ILeaderboardModelMapper>();

            _simerKeyReposity = Substitute.For<ISimerKeyRepository>();

            _service = Substitute.For<ILeaderBoardService>();

            _authenticatePayload = Substitute.For<IAuthenticatePayloadService>();
        }

        private LeaderboardController GetTarget()
        {
            return new LeaderboardController(_mapper, _authenticatePayload, _service);
        }

        [Fact]
        public void Should_post_metrics_without_failing()
        {
            // ARRANGE
            var original = new ApiModel()
            {
                SimerKey = "OkKey"
            };

            original.PilotName = "Pilot1";
            original.SimerKey = "OkKey";
            original.CarName = "SomeCarName";

            _mapper.Map(Arg.Any<ApiModel>())
                .Returns(c => new BusinessModel()
                {
                    PilotName = new Models.PilotName(c.Arg<ApiModel>().PilotName)
                });

            // ACT
            var target = GetTarget();

            target.Post(original);

            _service
                .Received(1)
                .Update(Arg.Is<IBusinessModel>(c => c.PilotName.ToString() == "Pilot1"));
        }

        [Fact]
        public void GIVEN_simerKey_received_equals_simerKey_configured_THEN_return_denied()
        {
            _simerKeyReposity.Key.Returns("other");

            var original = new LeaderboardModel()
            {
                SimerKey = "Key1"
            };

            _authenticatePayload.ValidatePayload(Arg.Is<ICallerInfos>(arg => arg.SimerKey == "Key1"))
              .Throws(new PostMetricDeniedException(original));

            var target = GetTarget();

            var intermediary = target.Post(original);

            var actual = (UnauthorizedObjectResult)intermediary;

            Check.That(actual.StatusCode).IsEqualTo(401);
        }

        [Fact]
        public void GIVEN_noPilotName_THEN_return_badRequest_AND_original_payload()
        {
            _simerKeyReposity.Key.Returns("Key1");

            var original = new LeaderboardModel()
            {
                SimerKey = "Key1"
            };
            
            original.PilotName = null;

            _authenticatePayload.ValidatePayload(
                 Arg.Is<ICallerInfos>(
                     arg => arg.SimerKey == "Key1"
                     && arg.PilotName == null)
             )
             .Returns(new List<string> { "Pilot name is mandatory." });

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

            var original = new LeaderboardModel()
            {
                SimerKey = "Key1"
            };

            original.SimerKey = "Key1";
            original.PilotName = "somePilot";
            original.CarName = null;

            _authenticatePayload.ValidatePayload(
                    Arg.Is<ICallerInfos>(
                        arg => arg.SimerKey == "Key1"
                        && arg.PilotName == "somePilot"
                        && arg.CarName == null)
                )
                .Returns(new List<string> { "Car name is mandatory." });

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
