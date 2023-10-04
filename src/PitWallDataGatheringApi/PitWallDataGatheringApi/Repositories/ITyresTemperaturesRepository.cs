﻿using PitWallDataGatheringApi.Models.Business;

namespace PitWallDataGatheringApi.Repositories
{
    public interface ITyresTemperaturesRepository
    {
        void UpdateFrontLeft(ITyresTemperatures? data);
        void UpdateFrontRight(ITyresTemperatures? data);
        void UpdateRearLeft(ITyresTemperatures? data);
        void UpdateRearRight(ITyresTemperatures? data);
    }
}