using NFluent;
using PitWallDataGatheringApi.Repositories.Prometheus;

namespace PitWallDataGatheringApi.Tests.Repositories.Prometheus
{
    public sealed class GaugWrapperTest
    {
        private const string Label1 = "label1";
        private const string Label2 = "label2";
        private readonly string[] EmptyLabels = new string[0];
        private readonly string[] NotEmptyLabels = new[] { Label1, Label2 };

        [Fact]
        public void GIVEN_serie_isNull_THEN_fail()
        {
            Check.ThatCode(() => new GaugeWrapper(
                    null,
                    "some_description",
                    NotEmptyLabels))
                .Throws<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'serieName')");
        }

        [Fact]
        public void GIVEN_serie_isEmpty_THEN_fail()
        {
            Check.ThatCode(() => new GaugeWrapper(
                    "",
                    "some_description",
                    NotEmptyLabels))
                .Throws<ArgumentException>()
                .WithMessage("Parameter 'serieName' cannot be empty.");
        }

        [Fact]
        public void GIVEN_documentation_isNull_THEN_fail()
        {
            Check.ThatCode(() => new GaugeWrapper(
                    "some_metric",
                    null,
                    NotEmptyLabels))
                .Throws<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'description')");
        }

        [Fact]
        public void GIVEN_description_isEmpty_THEN_fail()
        {
            Check.ThatCode(() => new GaugeWrapper(
                    "some_metrics",
                    "",
                    NotEmptyLabels))
                .Throws<ArgumentException>()
                .WithMessage("Parameter 'description' cannot be empty.");
        }

        [Fact]
        public void GIVEN_labels_isNull_THEN_fail()
        {
            Check.ThatCode(() => new GaugeWrapper(
                    "some_metric",
                    "some_description",
                    null))
                .Throws<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'labels')");
        }

        [Fact]
        public void GIVEN_labels_isEmpty_THEN_fail()
        {
            Check.ThatCode(() => new GaugeWrapper(
                    "some_metric",
                    "some_description",
                    EmptyLabels))
                .Throws<ArgumentException>()
                .WithMessage("Parameter 'labels' cannot be empty.");
        }

        private GaugeWrapper GetTarget()
        {
            return new GaugeWrapper("SomeSerieName", "SomeDescription", NotEmptyLabels);
        }

        [Fact]
        public void GIVEN_label_notDeclared_THEN_fail_update()
        {
            var target = GetTarget();

            Check.ThatCode(() =>
            target.Update("NotDeclaredLabel", 2.0))
                .Throws<LabelNotDeclaredException>()
                .WithProperty("LabelName", "NotDeclaredLabel")
                .And
                .WithProperty("Value", 2.0);
        }
    }
}
