﻿using PitWallDataGatheringApi.Models;

namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public interface IAvgWetnessRepository : IMetricRepository, IMetricRepositoryV2<double?>
    {
    }
}