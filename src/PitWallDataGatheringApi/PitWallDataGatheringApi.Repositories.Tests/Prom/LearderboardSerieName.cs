using System.Text.RegularExpressions;

namespace PitWallDataGatheringApi.Repositories.Tests.Prom
{
    public struct LearderboardSerieName
    {
        private const int DefaultPositionLast = 999;

        private static readonly Regex _metricMatcher = new Regex("pitwall_leaderboard_position(\\{0\\})_.*", RegexOptions.Compiled);

        //pitwall_leaderboard_position(\{0\})_.*
        public LearderboardSerieName(string description, int position, string metricFormat)
        {
            /**
             * Sanitize metric format null check + not empty
             * 
             * */

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

            var matches = _metricMatcher.Matches(description);

            if(!_metricMatcher.IsMatch(metricFormat))
            {
                throw new MetricNameFormatInvalidException(metricFormat);
            }

            MetricName = string.Format(metricFormat, Position.ToString("D2"));
        }

        public string Description { get; }
        public int Position { get; }
        public string MetricName { get; }
    }
}
