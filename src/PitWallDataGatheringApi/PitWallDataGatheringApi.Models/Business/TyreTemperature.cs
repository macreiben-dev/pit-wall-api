﻿namespace PitWallDataGatheringApi.Models.Business;


public class TyresTemperatures : ITyresTemperatures
{
    public double? FrontLeftTemp { get; set; }
    public double? FrontRightTemp { get; set; }
    public double? RearLeftTemp { get; set; }
    public double? RearRightTemp { get; set; }
}
