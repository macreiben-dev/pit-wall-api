﻿using PitWallDataGatheringApi.Models.Apis;
using Prometheus;

namespace PitWallDataGatheringApi.Repositories
{
    public sealed class TyreWearRepository : ITyreWearRepository
    {
        private readonly Gauge _gaugeTyre;
        readonly string[] tyreLabels = new[] { "TyreWear" };

        public TyreWearRepository()
        {
            var configTyres = new GaugeConfiguration();
            configTyres.LabelNames = tyreLabels;
            _gaugeTyre = Metrics.CreateGauge("pitwall_tyres_percent", "Tyres information.", configTyres);
        }

        public void UpdateFrontLeft(Tyres? tyresWears)
        {
            UpdateGauge(tyresWears.FrontLeftWear, "FrontLeft", _gaugeTyre);
        }

        public void UpdateFrontRight(Tyres? tyresWears)
        {
            UpdateGauge(tyresWears.FrontRightWear, "FrontRight", _gaugeTyre);
        }

        public void UpdateRearLeft(Tyres? tyresWears)
        {
            UpdateGauge(tyresWears.ReartLeftWear, "RearLeft", _gaugeTyre);
        }

        public void UpdateRearRight(Tyres? tyresWears)
        {
            UpdateGauge(tyresWears.RearRightWear, "RearRight", _gaugeTyre);
        }

        private void UpdateGauge(double? data, string gaugeLabel, Gauge gauge)
        {
            if (!data.HasValue)
            {
                return;
            }

            gauge.WithLabels(gaugeLabel).Set(data.Value);
        }
    }
}
