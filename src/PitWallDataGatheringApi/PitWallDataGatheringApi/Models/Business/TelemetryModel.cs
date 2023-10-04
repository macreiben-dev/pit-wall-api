﻿namespace PitWallDataGatheringApi.Models.Business
{
    public sealed class TelemetryModel : ITelemetryModel
    {
        public TelemetryModel()
        {
            TyresWear = new TyresWear();
        }
        public string PilotName { get; set; }
        public double? LaptimeSeconds { get; set; }

        public ITyresWear? TyresWear { get; set; }
    }
}