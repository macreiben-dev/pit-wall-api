namespace PitWallDataGatheringApi.Models.Apis.v1.Leaderboards
{
    /**
     * Actual name in the acquisition plugin example:
        GarySwallowDataPlugin.Leaderboard.Position10.DriverName

        GarySwallowDataPlugin.Leaderboard.Position10.CarClass
        GarySwallowDataPlugin.Leaderboard.Position10.CarNumber

        GarySwallowDataPlugin.Leaderboard.Position10.LastLap
        GarySwallowDataPlugin.Leaderboard.Position10.LastPitLap

        GarySwallowDataPlugin.Leaderboard.Position10.LastSector1
        GarySwallowDataPlugin.Leaderboard.Position10.LastSector2
        GarySwallowDataPlugin.Leaderboard.Position10.LastSector3

        GarySwallowDataPlugin.Leaderboard.Position10.DQ
        GarySwallowDataPlugin.Leaderboard.Position10.DNF

        GarySwallowDataPlugin.Leaderboard.Position10.IntervalGap

        GarySwallowDataPlugin.Leaderboard.Position01.PitCount

        GarySwallowDataPlugin.Leaderboard.Position10.InGarage
        GarySwallowDataPlugin.Leaderboard.Position10.InPitBox
        GarySwallowDataPlugin.Leaderboard.Position10.InPitLane
     * */

    public sealed class LeaderboardEntry
    {
        public int Position { get; set; }

        public string? CarClass { get; set; }

        public string? CarNumber { get; set; }
        public string? CarName { get; set; }

        public double? LastLapInSeconds { get; set; }

        public int  LastPitLap { get; set; }

        public int PitCount { get; set; }

        public double? LastSector1InSeconds { get; set; }
        public double? LastSector2InSeconds { get; set; }
        public double? LastSector3InSeconds { get; set; }

        public bool InGarage { get; set; }
        public bool InPitBox { get; set; }
        public bool InPitLane { get; set; }

        public bool DNF { get; set; }
        public bool DQ { get; set; }
    }
}
