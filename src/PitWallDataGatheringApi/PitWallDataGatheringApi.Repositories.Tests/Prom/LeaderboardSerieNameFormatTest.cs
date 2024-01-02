using NFluent;

namespace PitWallDataGatheringApi.Repositories.Tests.Prom
{
    public class LeaderboardSerieNameFormatTest
    {
        private const string DescriptionValue = "SomeDescription";
        private const int FirstInLeaderboard = 1;
        private const string MetricFirstInLeaderBoardCarNumberValue = "pitwall_leaderboard_position{0}_carNumber";

        private static LearderboardSerieName GetTarget()
        {
            return new LearderboardSerieName(DescriptionValue, FirstInLeaderboard, MetricFirstInLeaderBoardCarNumberValue);
        }

        [Fact]
        public void GIVEN_description_THEN_expose_description()
        {
            var target = GetTarget();

            Check.That(target.Description).IsEqualTo(DescriptionValue);
        }


        [Fact]
        public void GIVEN_description_isNull_THEN_fail()
        {
            Check.ThatCode(() => new LearderboardSerieName(null, FirstInLeaderboard, MetricFirstInLeaderBoardCarNumberValue))
                .Throws<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-10)]
        [InlineData(-254123)]
        public void GIVEN_position_isNegativ_OR_zero_THEN_expose999(int position)
        {
            var target = new LearderboardSerieName(DescriptionValue, position, MetricFirstInLeaderBoardCarNumberValue);

            Check.That(target.Position).IsEqualTo(999);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(10)]
        [InlineData(254123)]
        public void GIVEN_position_isPositiv_THEN_expose_position(int position)
        {
            var target = new LearderboardSerieName(DescriptionValue, position, MetricFirstInLeaderBoardCarNumberValue);

            Check.That(target.Position).IsEqualTo(position);
        }

        [Theory]
        [InlineData("pitwall_leaderboard_position{0}_carNumber", "pitwall_leaderboard_position01_carNumber")]
        [InlineData("pitwall_leaderboard_position{0}_carClass",  "pitwall_leaderboard_position01_carClass")]
        [InlineData("pitwall_leaderboard_position{0}_position",  "pitwall_leaderboard_position01_position")]
        public void GIVEN_metricFomrat_THEN_ensure_position_positioner_exists(string metricFormat, string expected)
        {
            var target = new LearderboardSerieName(DescriptionValue, FirstInLeaderboard, metricFormat);

            Check.That(target.MetricName).IsEqualTo(expected);
        }

        [Theory]
        [InlineData("pitwall_leaderboard_position01_carNumber")]
        [InlineData("pitwall_leaderboard_position01_carClass")]
        [InlineData("pitwall_leaderboard_position01_position")]
        public void GIVEN_metricName_withNoFormat_positioner_THEN_fail(string metricFormat)
        {
            Check.ThatCode(() => new LearderboardSerieName(DescriptionValue, FirstInLeaderboard, metricFormat))
                .Throws<MetricNameFormatInvalidException>()
                .WithProperty("MetricName", metricFormat);
        }

        [Fact]
        public void GIVEN_metricName_isEmpty_THEN_fail()
        {
            Check.ThatCode(() => new LearderboardSerieName(DescriptionValue, FirstInLeaderboard, string.Empty))
              .Throws<ArgumentException>()
              .WithProperty("Message", "metricFormat");
        }

        [Fact]
        public void GIVEN_metricName_isNull_THEN_fail()
        {
            Check.ThatCode(() => new LearderboardSerieName(DescriptionValue, FirstInLeaderboard, null))
              .Throws<ArgumentNullException>()
              .WithProperty("Message", "Value cannot be null. (Parameter 'metricFormat')");
        }
    }
}
