﻿using PitWallDataGatheringApi.Models.Apis;

namespace PitWallDataGatheringApi.Integration.Tests
{
    public interface IContext
    {
        object Expected { get; }
        string MetricName { get; }
        string PilotName { get; }
        string CarName { get; }
        string SimerKey { get; }

        Action<TelemetryModel> SetFieldValue
        {
            get;
        }
        Func<TelemetryModel> GetApiModelInstance
        {
            get;
        }
    }
}