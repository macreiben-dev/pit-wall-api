using Microsoft.Extensions.Logging;
using NFluent;
using NSubstitute;
using PitWallDataGatheringApi.Repositories.Gauges;
using PitWallDataGatheringApi.Repositories.Gauges.Prom;

namespace PitWallDataGatheringApi.Tests.Repositories.Prometheus
{
    public class GaugeWrapperFactoryTest
    {
        [Fact]
        public void THEN_create_gauge()
        {
            Check.ThatCode(() => GetTarget().Create(
                "some_serie",
                "some description",
                new[] { "label1, label2" }
                ));
        }

        private static GaugeWrapperFactory GetTarget()
        {
            return new GaugeWrapperFactory(Substitute.For<ILogger<GaugeWrapperFactory>>(),
                Substitute.For<ILogger<GaugeWrapper>>());
        }

        [Fact]
        public void GIVEN_gauge_created_THEN_list_createGauge()
        {
            var target = GetTarget();

            var serie1 = new SerieDescriptor()
            {
                SerieName = "some_serie_01",
                SerieDescription = "some description 01",
                Labels = new[] { "label1", "label2" }
            };

            var serie2 = new SerieDescriptor()
            {
                SerieName = "some_serie_02",
                SerieDescription = "some description 02",
                Labels = new[] { "label3", "label4" }
            };

            foreach (var serie in new[] { serie1, serie2 })
            {
                target.Create(serie.SerieName,
                    serie.SerieDescription,
                    serie.Labels);
            }

            IEnumerable<ISerieDocumentation> actual = target.ListCreated();

            Check.That(actual).HasSize(2);

            Check.That(actual).HasElementThatMatches((serie) => Matches(serie));

        }

        private static bool Matches(ISerieDocumentation serie)
        {
            return serie.SerieName == "some_serie_01"
                            && serie.Description == "some description 01"
                            && serie.Labels.Contains("label1")
                            && serie.Labels.Contains("label2")
                            && serie.Labels.Count() == 2;
        }

        [Fact]
        public void GIVEN_gauge_create_AND_same_serie_toBeCreated_THEN_return_previously_created_gauge()
        {
            var target = GetTarget();

            var serie = new SerieDescriptor()
            {
                SerieName = "Serie01",
                SerieDescription = "Some serie description",
                Labels = new[] { "label1", "label2" }
            };

            var actualInitial = target.Create(serie.SerieName,
                serie.SerieDescription,
                serie.Labels);

            var actualSecond = target.Create(serie.SerieName,
                serie.SerieDescription,
                serie.Labels);

            Check.That(actualSecond.GetHashCode()).IsEqualTo(actualInitial.GetHashCode());
        }

        [Theory]
        [InlineData(1, "some_position01_inrace_metric")]
        [InlineData(2, "some_position02_inrace_metric")]
        [InlineData(33, "some_position33_inrace_metric")]
        [InlineData(999, "some_position999_inrace_metric")]
        public void GIVEN_serieNameFormat_AND_description_AND_positionInRace_THEN_generate_one_gauge_AND_metricName_contains_position(
            int position, 
            string expected)
        {
            string originalFormat = "some_position{0}_inrace_metric";
            string description = "some description";
            int positionInRace = position;

            var target = GetTarget();

            var actual = target.CreateLeaderboardGauge(
                 originalFormat,
                 description,
                 positionInRace,
                 new[] { "label1", "label2" });

            Check.That(actual.SerieName).IsEqualTo(expected);
        }

        [Theory]
        [InlineData(1, "some_description01")]
        [InlineData(2, "some_description02")]
        [InlineData(33, "some_description33")]
        [InlineData(999, "some_description999")]
        public void GIVEN_serieNameFormat_AND_description_AND_positionInRace_THEN_generate_one_gauge_AND_description_contains_position(
            int position,
            string expected)
        {
            string originalFormat = "some_position{0}_inrace_metric";
            string description = "some_description{0}";
            int positionInRace = position;

            var target = GetTarget();

            var actual = target.CreateLeaderboardGauge(
                 originalFormat,
                 description,
                 positionInRace,
                 new[] { "label1", "label2" });

            Check.That(actual.Description).IsEqualTo(expected);
        }

        [Fact]
        public void GIVEN_gauge_create_AND_serieNameFormat_toBeCreated_THEN_return_previously_created_gauge()
        {
            var target = GetTarget();

            var serie = new SerieDescriptor()
            {
                SerieName = "Serie01",
                SerieDescription = "Some serie description",
                Labels = new[] { "label1", "label2" }
            };

            string originalFormat = "some_position{0}_inrace_metric";
            string description = "some_description{0}";

            var actualInitial = target.CreateLeaderboardGauge(originalFormat,
                description,
                13,
                serie.Labels);

            var actualSecond = target.CreateLeaderboardGauge(originalFormat,
                description,
                13,
                serie.Labels);

            Check.That(actualSecond.GetHashCode()).IsEqualTo(actualInitial.GetHashCode());
        }
    }

    public readonly record struct SerieDescriptor(
        string SerieName,
        string SerieDescription,
        string[] Labels);
}
