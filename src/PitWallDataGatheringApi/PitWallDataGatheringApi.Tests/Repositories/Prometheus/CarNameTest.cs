using NFluent;
using PitWallDataGatheringApi.Repositories.Prometheus;

namespace PitWallDataGatheringApi.Tests.Repositories.Prometheus
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
    }
}
