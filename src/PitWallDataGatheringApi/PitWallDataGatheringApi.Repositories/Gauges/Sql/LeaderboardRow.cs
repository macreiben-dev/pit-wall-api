using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitWallDataGatheringApi.Repositories.Gauges.Sql
{
    internal class LeaderboardRow
    {
        public string? PilotName { get; set; }

        public string? CarName { get; set; }

        public int Data_Tick { get; set; }

        public string MetricName { get; set; }

        public string MetricValue { get; set; }
    }
}
