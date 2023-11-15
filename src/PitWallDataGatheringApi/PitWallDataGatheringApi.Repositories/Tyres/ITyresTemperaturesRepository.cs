﻿using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Models.Business;

namespace PitWallDataGatheringApi.Repositories.Tyres
{
    public interface ITyresTemperaturesRepository
    {
        void UpdateFrontLeft(string pilotName, double? frontLeftTemp, CarName carName);

        void UpdateFrontRight(string pilotName, double? frontRightTemp, CarName carName);

        void UpdateRearLeft(string pilotName, double? rearLeftTemp, CarName carName);

        void UpdateRearRight(string pilotName, double? rearRightTemp, CarName carName);
    }
}