using NFluent;

namespace PitWallDataGatheringApi.Repositories.Tests.Prom
{
    public class LeaderboardSerieNameFormatTest
    {
        private const string DescriptionValue = "SomeDescription";
        private const int FirstInLeaderboard = 1;

        private static LearderboardSerieName GetTarget()
        {
            return new LearderboardSerieName(DescriptionValue, FirstInLeaderboard);
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
            Check.ThatCode(() => new LearderboardSerieName(null, FirstInLeaderboard))
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
            var target = new LearderboardSerieName(DescriptionValue, position);

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
            var target = new LearderboardSerieName(DescriptionValue, position);

            Check.That(target.Position).IsEqualTo(position);
        }
    }

    public struct LearderboardSerieName
    {
        private const int DefaultPositionLast = 999;

        public LearderboardSerieName(string description, int position)
        {
            if (description == null) throw new ArgumentNullException(nameof(description));

            Description = description;

            Position = position;

            if (position < 1)
            {
                Position = DefaultPositionLast;
            }
            else
            {
                Position = position;
            }
        }

        public string Description { get; }
        public int Position { get; }
    }
}
