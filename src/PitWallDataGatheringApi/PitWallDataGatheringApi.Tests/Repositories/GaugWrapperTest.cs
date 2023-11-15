using NFluent;
using PitWallDataGatheringApi.Repositories.WeatherConditions;

namespace PitWallDataGatheringApi.Tests.Repositories
{
    public sealed class GaugWrapperTest
    {
        [Fact]
        public void GIVEN_serie_isNull_THEN_fail()
        {
            Check.ThatCode(() => new GaugeWrapper(
                    null, 
                    "some_description", 
                    new[] { "label1", "label2" }))
                .Throws<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'serieName')");
        }

        [Fact]
        public void GIVEN_serie_isEmpty_THEN_fail()
        {
            Check.ThatCode(() => new GaugeWrapper(
                    "",
                    "some_description",
                    new[] { "label1", "label2" }))
                .Throws<ArgumentException>()
                .WithMessage("Parameter 'serieName' cannot be empty.");
        }

        [Fact]
        public void GIVEN_documentation_isNull_THEN_fail()
        {
            Check.ThatCode(() => new GaugeWrapper(
                    "some_metric",
                    null,
                    new[] { "label1", "label2" }))
                .Throws<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'description')");
        }

        [Fact]
        public void GIVEN_description_isEmpty_THEN_fail()
        {
            Check.ThatCode(() => new GaugeWrapper(
                    "some_metrics",
                    "",
                    new[] { "label1", "label2" }))
                .Throws<ArgumentException>()
                .WithMessage("Parameter 'description' cannot be empty.");
        }
    }
}
