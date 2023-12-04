using NFluent;
using PitWallDataGatheringApi.Models;

namespace PitWallDataGatheringApi.Tests.Models
{
    public sealed class CarNameTest
    {
        [Fact]
        public void GIVEN_car_isNotNull_THEN_expose_carName()
        {
            var target = new CarName("32");

            string actual = target.ToString();

            Check.That(actual).IsEqualTo("32");
        }


        [Fact]
        public void GIVEN_car_isNull_THEN_expose_noCarNumber()
        {
            var target = new CarName(null);

            string actual = target.ToString();

            Check.That(actual).IsEqualTo("NoCarNumber");
        }

        [Fact]
        public void WHEN_nullInvoked_THEN_return_null_equivalent()
        {
            var actual = CarName.Null();

            Check.That(actual.ToString()).IsEqualTo("NoCarNumber");
        }

        [Fact]
        public void GIVEN_pilotName_equal_pilotName_WHEN_equalOperator_THEN_true()
        {
            var left = new CarName("32");
            var right = new CarName("32");

            var actual = left == right;

            Check.That(actual).IsTrue();
        }

        [Fact]
        public void GIVEN_pilotName_notEqual_pilotName_WHEN_notEqualOperator_THEN_true()
        {
            var left = new CarName("32");
            var right = new CarName("322");

            var actual = left != right;

            Check.That(actual).IsTrue();
        }


        [Fact]
        public void GIVEN_pilotName_notequal_pilotName_WHEN_equal_invoked_THEN_false()
        {
            var left = new CarName("32");
            CarName? right = null;

            var actual = left.Equals(right);

            Check.That(actual).IsFalse();
        }
    }
}
