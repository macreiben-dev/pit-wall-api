using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;
using PitWallDataGatheringApi.Controllers;
using PitWallDataGatheringApi.Controllers.v1;
using PitWallDataGatheringApi.Models.Apis.v1.Leaderboards;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Services;

namespace PitWallDataGatheringApi.Tests.Controllers
{
    public class LeaderboardControllerTest
    {
        private ILeaderboardModelMapper _mapper;
        private ISimerKeyRepository _simerKeyReposity;
        private ILeaderBoardService _leaderboardService;

        public LeaderboardControllerTest()
        {
            _mapper = Substitute.For<ILeaderboardModelMapper>();

            _simerKeyReposity = Substitute.For<ISimerKeyRepository>();

            _leaderboardService = Substitute.For<ILeaderBoardService>();
        }

        private LeaderboardController GetTarget()
        {
            return new LeaderboardController(_simerKeyReposity, _mapper);
        }

        [Fact]
        public void GIVEN_simerKey_received_equals_simerKey_configured_THEN_return_denied()
        {
            _simerKeyReposity.Key.Returns("other");

            var original = new LeaderboardModel();

            original.SimerKey = "Key1";

            var target = GetTarget();

            var intermediary = target.Post(original);

            var actual = (UnauthorizedObjectResult)intermediary;

            Check.That(actual.StatusCode).IsEqualTo(401);
        }

        [Fact]
        public void GIVEN_noPilotName_THEN_return_badRequest_AND_original_payload()
        {
            _simerKeyReposity.Key.Returns("Key1");

            var original = new LeaderboardModel();

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

            var original = new LeaderboardModel();

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
