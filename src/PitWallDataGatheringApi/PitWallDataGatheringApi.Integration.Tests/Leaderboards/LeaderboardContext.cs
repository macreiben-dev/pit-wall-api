using PitWallDataGatheringApi.Models.Apis.v1.Leaderboards;

namespace PitWallDataGatheringApi.Integration.Tests.Leaderboards
{
    public sealed class LeaderboardContext
    {
        public const string TimeSerieUri = "http://localhost:10100";
        public const string TargetApi = "http://localhost:32773";
        public const string SimerKey = "some_test_looking_value23";

        private readonly IList<LeaderboardEntry> _entries = new List<LeaderboardEntry>();

        public LeaderboardContext()
        {
        }

        public string PilotName { get; set; }

        public string CarName
        {
            get; set;
        }

        public string MetricNameToAssert { get; private set; }

        public double MetricValueToAssert { get; private set; }

        public LeaderboardContext Add(LeaderboardEntry entry)
        {
            _entries.Add(entry);

            return this;
        }

        public LeaderboardContext WithPilotName(string? pilotName) { 
        
            PilotName = pilotName;  

            return this;
        }

        public LeaderboardContext WithCarName(string? carName)
        {
            CarName = carName;
            return this;
        }
        
        public LeaderboardContext ShouldAssertMetric(string metricName, double expected)
        {
            MetricNameToAssert = metricName;
            MetricValueToAssert = expected;

            return this;
        }

        public LeaderboardModel AsApiModel()
        {
            LeaderboardModel model = new LeaderboardModel()
            {
                CarName = CarName,
                PilotName = PilotName,
                SimerKey = SimerKey,
                Entries = _entries.ToList()
            };

            return model;
        }


    }
}
