using NFluent;
using NSubstitute;
using PitWallDataGatheringApi.Models.Apis.v1;
using PitWallDataGatheringApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitWallDataGatheringApi.Services.Tests
{
    public class AuthenticatePayloadServiceTest
    {
        private const string SetupKey = "someKey";
        private ISimerKeyRepository _simerKeys;

        public AuthenticatePayloadServiceTest()
        {

            _simerKeys = Substitute.For<ISimerKeyRepository>();
            _simerKeys.Key.Returns(SetupKey);

        }

        private AuthenticatePayloadService GetTarget()
        {
            return new AuthenticatePayloadService(_simerKeys);
        }

        [Fact]
        public void GIVEN_simerKey_notEqual_configured_simerKey_THEN_throw_postMetricDeniedException()
        {

            var original = new FakeCallerInfos()
            {
                SimerKey = "someOtherKey"
            };

            var target = GetTarget();

            Check.ThatCode(() => target.ValidatePayload(original))
                .Throws<PostMetricDeniedException>();
        }

        [Fact]
        public void GIVEN_simerKey_valid_AND_pilotName_isNull_THEN_addMessage()
        {
            var original = new FakeCallerInfos()
            {
                SimerKey = SetupKey,
                PilotName = null,
                CarName = "SomeCar"
            };

            var target = GetTarget();

            var actual = target.ValidatePayload(original);

            Check.That(actual.First()).IsEqualTo("Pilot name is mandatory.");
        }

        [Fact]
        public void GIVEN_simerKey_valid_AND_carName_isNull_THEN_addMessage()
        {
            var original = new FakeCallerInfos()
            {
                SimerKey = SetupKey,
                PilotName = "somePilot",
                CarName = null
            };

            var target = GetTarget();

            var actual = target.ValidatePayload(original);

            Check.That(actual.First()).IsEqualTo("Car name is mandatory.");
        }
    }
}
