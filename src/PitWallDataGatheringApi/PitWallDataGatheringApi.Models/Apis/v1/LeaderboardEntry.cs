using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitWallDataGatheringApi.Models.Apis.v1
{
    /*
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

        public string CarClass { get; set; }

        public string CarNumber { get; set; }

        public double? LastLapInSeconds { get; set; }

        public string LastPitLap { get; set; }

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
