namespace PitWallDataGatheringApi.Repositories.Tests.Prom
{
    public struct LearderboardSerieName
    {
        private const int DefaultPositionLast = 999;
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

            MetricName = string.Format(metricFormat, Position.ToString("D2"));
        }

        public string Description { get; }
        public int Position { get; }
        public string MetricName { get; }
    }
}
