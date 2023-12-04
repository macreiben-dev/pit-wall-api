using NFluent;
using PitWallDataGatheringApi.Models;

namespace PitWallDataGatheringApi.Tests.Models
{
    public class PilotNameTest
    {
        [Fact]
        public void GIVEN_pilotName_isNotNull_THEN_expose_pilotName()
        {
            var target = new PilotName("pilotName");

            string actual = target.ToString();

            Check.That(actual).IsEqualTo("pilotName");
        }

        [Fact]
        public void GIVENpilotName_isNull_THEN_expose_noPilotName()
        {
            var target = new PilotName(null);

            string actual = target.ToString();

            Check.That(actual).IsEqualTo("NoPilotName");
        }

        [Fact]
        public void WHEN_nullInvoked_THEN_return_null_equivalent()
        {
            var actual = PilotName.Null();

            Check.That(actual.ToString()).IsEqualTo("NoPilotName");
        }

        [Fact]
        public void GIVEN_pilotName_equal_pilotName_WHEN_equalOperator_THEN_true()
        {
            var left = new PilotName("pilotName");
            var right = new PilotName("pilotName");

            var actual = left == right;

            Check.That(actual).IsTrue();
        }

        [Fact]
        public void GIVEN_pilotName_notEqual_pilotName_WHEN_notEqualOperator_THEN_true()
        {
            var left = new PilotName("pilotName");
            var right = new PilotName("pilotName2");

            var actual = left != right;

            Check.That(actual).IsTrue();
        }


        [Fact]
        public void GIVEN_pilotName_notequal_pilotName_WHEN_equal_invoked_THEN_false()
        {
            var left = new PilotName("pilotName");
            PilotName? right = null;

            var actual = left.Equals(right);

            Check.That(actual).IsFalse();
        }

    }
}
