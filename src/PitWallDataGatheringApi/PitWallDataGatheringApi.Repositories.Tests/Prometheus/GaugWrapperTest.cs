using NFluent;
using PitWallDataGatheringApi.Repositories.Prometheus;

namespace PitWallDataGatheringApi.Tests.Repositories.Prometheus
{
    public sealed class GaugeWrapperTest
    {
        private const string Description = "SomeDescription";
        private const string SerieName = "SomeSerieName";

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
            return new GaugeWrapper(SerieName, Description, NotEmptyLabels);
        }


        [Fact]
        public void GIVEN_multiple_labels_AND_labels_count_is_different_THEN_fail()
        {
            var target = GetTarget();

            Check.ThatCode(() =>
                target.Update(new[] { Label1 }, 13.2))
                .Throws<LabelCountMustMatchDeclaredLabelsException>();
        }


        [Fact]
        public void GIVEN_multiple_labels_AND_unpdate_singleLabel_THEN_fail()
        {
            var target = GetTarget();

            Check.ThatCode(() =>
                target.Update(Label1, 13.2))
                .Throws<LabelCountMustMatchDeclaredLabelsException>();
        }

        [Fact]
        public void THEN_gauge_exposes_label()
        {
            var target = GetTarget();

            Check.That(target.Labels).ContainsExactly(NotEmptyLabels);
        }

        [Fact]
        public void THEN_gauge_exposes_description()
        {
            var target = GetTarget();

            Check.That(target.Description).IsEqualTo(Description);
        }

        [Fact]
        public void THEN_gauge_exposes_serieName()
        {
            var target = GetTarget();

            Check.That(target.SerieName).IsEqualTo(SerieName);
        }

    }
}
