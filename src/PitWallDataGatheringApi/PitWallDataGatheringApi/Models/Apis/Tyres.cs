﻿namespace PitWallDataGatheringApi.Models.Apis
{
    public sealed class Tyres : ITyres
    {
        public double? FrontLeftWear { get; set; }

        public double? FrontRightWear { get; set; }

        public double? ReartLeftWear { get; set; }

        public double? RearRightWear { get; set; }
    }
}