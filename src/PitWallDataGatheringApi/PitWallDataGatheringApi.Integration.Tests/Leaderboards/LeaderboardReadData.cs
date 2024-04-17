namespace PitWallDataGatheringApi.Integration.Tests.Leaderboards
{
    public sealed class LeaderboardReadData
    {
        public string? source_pilot_name { get; set; }
        
        public string? source_car_name { get; set; }

        public long? data_tick { get; set; }

        public string? metric_position { get; set; }
        
        public string? metric_car_number { get; set; }
        
        public string? metric_car_class { get; set; }
    }
}