using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Prom;
using PitWallDataGatheringApi.Repositories.VehicleConsumptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitWallDataGatheringApi.Repositories.Leaderboards
{
    public class LeaderboardCarNumberRepository
    {
        private const string LocalSerieNameFormat = "pitwall_leaderboard_position{0}_carnumber";
        private readonly IGauge _gaugeLapTimes;

        public LeaderboardCarNumberRepository(IGaugeFactory gaugeFactory)
        {
            //_gaugeLapTimes = gaugeFactory.Create(
            //    LocalSerieName,
            //    "Car laptimes in seconds.",
            //    ConstantLabels.Labels);
        }

        public void Update(double? data, string pilotName, CarName carName)
        {
            Update(new MetricData<double?>(data, new PilotName(pilotName), carName));
        }

        public void Update(MetricData<double?> metric)
        {
            MetricDataToGauge.Execute(_gaugeLapTimes, metric);
        }
    }
}
