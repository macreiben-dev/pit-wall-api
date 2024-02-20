﻿namespace PitWallDataGatheringApi.Models.Business.Leaderboards
{
    public class LeaderboardEntry : ILeaderboardEntry
    {
        public int LastPitLap { get; set; }

        public string CarClass { get; set; }

        public string CarNumber { get; set; }

        public int Position { get; set; }
    }
}