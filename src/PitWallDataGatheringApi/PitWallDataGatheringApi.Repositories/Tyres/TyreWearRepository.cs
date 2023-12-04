using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Prom;
using PitWallDataGatheringApi.Repositories.VehicleConsumptions;

namespace PitWallDataGatheringApi.Repositories.Tyres
{
    public sealed class TyreWearRepository : ITyreWearRepositoryLegacy, ITyreWearRepository
    {
        private const string GaugeNamePitwallTyreWearPercent = "pitwall_tyreswear_percent";

        private const string GaugeNameFrontLeft = "pitwall_tyres_wear_frontleft_percent";
        private const string GaugeNameRearLeft = "pitwall_tyres_wear_rearleft_percent";
        private const string GaugeNameFrontRight = "pitwall_tyres_wear_frontright_percent";
        private const string GaugeNameRearRight = "pitwall_tyres_wear_rearright_percent";

        private readonly IGauge _gaugeFrontLeft;
        private readonly IGauge _gaugeRearLeft;
        private readonly IGauge _gaugeFrontRight;
        private readonly IGauge _gaugeRearRight;

        public TyreWearRepository(IGaugeWrapperFactory gaugeFactory)
        {
            _gaugeFrontLeft = gaugeFactory.Create(GaugeNameFrontLeft, "Tyres wear front left in percent.", ConstantLabels.Labels);
            _gaugeRearLeft = gaugeFactory.Create(GaugeNameRearLeft, "Tyres wear front left in percent.", ConstantLabels.Labels);
            _gaugeFrontRight = gaugeFactory.Create(GaugeNameFrontRight, "Tyres wear front left in percent.", ConstantLabels.Labels);
            _gaugeRearRight = gaugeFactory.Create(GaugeNameRearRight, "Tyres wear front left in percent.", ConstantLabels.Labels);
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
