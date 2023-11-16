﻿using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Prometheus;

namespace PitWallDataGatheringApi.Repositories
{
    public sealed class LaptimeRepository : ILaptimeRepository
    {
        private const string LocalSerieName = "pitwall_laptimes_seconds";
        private readonly IGauge _gaugeLapTimes;
        readonly string[] pilotLabels = new[] { "Pilot" };

        public LaptimeRepository(IGaugeWrapperFactory gaugeFactory)
        {
            _gaugeLapTimes = gaugeFactory.Create(
                LocalSerieName,
                "Car laptimes in seconds.",
                ConstantLabels.Labels);
        }

        public void Update(double? dataValue, string pilotName, CarName carName)
        {
            _gaugeLapTimes.Update(new[] { pilotName, carName.ToString() }, dataValue);
        }
    }
}
