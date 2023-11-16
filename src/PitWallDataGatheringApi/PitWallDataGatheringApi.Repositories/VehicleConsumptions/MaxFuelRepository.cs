﻿using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Prometheus;

namespace PitWallDataGatheringApi.Repositories.VehicleConsumptions
{
    public sealed class MaxFuelRepository : IMaxFuelRepository
    {
        private const string LocalSerieName = "pitwall_maxfuel_volume";
        private readonly IGauge _gauge;

        private string Description => "MaxFuel volume of the tank.";


        public MaxFuelRepository(IGaugeWrapperFactory _gaugeFactory)
        {
            _gauge = _gaugeFactory.Create(
              LocalSerieName,
              Description,
              ConstantLabels.Labels);
        }

        public void Update(double? data, string pilotName, CarName carName)
        {
            _gauge.Update(new[] { pilotName, carName.ToString() }, data);
        }
    }
}
