﻿using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Gauges;
using PitWallDataGatheringApi.Repositories.Gauges.Prom;
using PitWallDataGatheringApi.Repositories.VehicleConsumptions;

namespace PitWallDataGatheringApi.Repositories.Tyres
{
    public sealed class TyresTemperaturesRepository
        : ITyresTemperaturesRepository
    {
        private const string GaugeNameFrontLeft = "pitwall_tyres_temperatures_frontleft_celsius";
        private const string GaugeNameRearLeft = "pitwall_tyres_temperatures_rearleft_celsius";
        private const string GaugeNameFrontRight = "pitwall_tyres_temperatures_frontright_celsius";
        private const string GaugeNameRearRight = "pitwall_tyres_temperatures_rearright_celsius";

        private readonly IGauge _gaugeFrontLeft;
        private readonly IGauge _gaugeRearLeft;
        private readonly IGauge _gaugeFrontRight;
        private readonly IGauge _gaugeRearRight;

        public TyresTemperaturesRepository(IGaugeFactory gaugeFactory)
        {
            _gaugeFrontLeft = gaugeFactory.Create(GaugeNameFrontLeft, "Front left tyre temperature in celsius.", ConstantLabels.Labels);
            _gaugeRearLeft = gaugeFactory.Create(GaugeNameRearLeft, "Front left tyre temperature in celsius.", ConstantLabels.Labels);
            _gaugeFrontRight = gaugeFactory.Create(GaugeNameFrontRight, "Front left tyre temperature in celsius.", ConstantLabels.Labels);
            _gaugeRearRight = gaugeFactory.Create(GaugeNameRearRight, "Front left tyre temperature in celsius.", ConstantLabels.Labels);
        }

        public void UpdateFrontLeft(double? data, string pilotName, CarName carName)
        {
            UpdateFrontLeft(new MetricData<double?>(data, new PilotName(pilotName), carName));
        }

        public void UpdateFrontLeft(MetricData<double?> metric)
        {
            MetricDataToGauge.Execute(_gaugeFrontLeft, metric);
        }

        public void UpdateFrontRight(double? data, string pilotName, CarName carName)
        {
            UpdateFrontRight(new MetricData<double?>(data, new PilotName(pilotName), carName));
        }

        public void UpdateFrontRight(MetricData<double?> metric)
        {
            MetricDataToGauge.Execute(_gaugeFrontRight, metric);
        }

        public void UpdateRearLeft(double? data, string pilotName, CarName carName)
        {
            UpdateRearLeft(new MetricData<double?>(data, new PilotName(pilotName), carName));
        }

        public void UpdateRearLeft(MetricData<double?> metric)
        {
            MetricDataToGauge.Execute(_gaugeRearLeft, metric);
        }

        public void UpdateRearRight(double? data, string pilotName, CarName carName)
        {
            UpdateRearRight(new MetricData<double?>(data, new PilotName(pilotName), carName));
        }

        public void UpdateRearRight(MetricData<double?> metric)
        {
            MetricDataToGauge.Execute(_gaugeRearRight, metric);
        }
    }
}
