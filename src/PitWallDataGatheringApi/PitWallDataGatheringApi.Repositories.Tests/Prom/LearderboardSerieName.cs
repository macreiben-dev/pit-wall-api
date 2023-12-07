using System.Text.RegularExpressions;

namespace PitWallDataGatheringApi.Repositories.Tests.Prom
{
    public struct LearderboardSerieName
    {
        private const int DefaultPositionLast = 999;
        private const int FirstPosition = 1;
        private const string IntegerFormat2LeadingZeros = "D2";
        private static readonly Regex _metricMatcher = new Regex("pitwall_leaderboard_position(\\{0\\})_.*", RegexOptions.Compiled);

        public LearderboardSerieName(string description, int position, string metricFormat)
        {
            if (description == null) throw new ArgumentNullException(nameof(description));

            if(metricFormat == string.Empty) throw new ArgumentException(nameof(metricFormat));

            if (metricFormat == null) throw new ArgumentNullException(nameof(metricFormat));

            Description = description;

            if (position < FirstPosition)
            {
                Position = DefaultPositionLast;
            }
            else
            {
                Position = position;
            }

            /**
             * thought : no limit for the moment on the max number of pilots in a race.
             * 
             * */

            var matches = _metricMatcher.Matches(description);

            if(!_metricMatcher.IsMatch(metricFormat))
            {
                throw new MetricNameFormatInvalidException(metricFormat);
            }

            MetricName = string.Format(metricFormat, Position.ToString(IntegerFormat2LeadingZeros));
        }

        public string Description { get; }
        public int Position { get; }
        public string MetricName { get; }
    }
}
