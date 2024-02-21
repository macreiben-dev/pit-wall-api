using System.Threading.Tasks.Dataflow;

namespace PitWallDataGatheringApi.Integration.Tests.Leaderboards
{
    public sealed class LeaderboardReadData
    {
        public string? pilot_name { get; set; }
        
        public string? car_name { get; set; }

        public long? data_tick { get; set; }

        public string? metric_name { get; set; }

        public string? metric_value { get; set; }
    }
}