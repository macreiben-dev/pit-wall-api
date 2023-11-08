using PitWallDataGatheringApi.Models.Business;
using Prometheus;

namespace PitWallDataGatheringApi.Repositories.Tyres
{
    public sealed class TyreWearRepository : ITyreWearRepository, IDocumentationTyresWearSerie
    {
        private const string GaugeNamePitwallTyreWearPercent = "pitwall_tyreswear_percent";

        private const string GaugeNameFrontLeft = "pitwall_tyres_wear_frontleft_percent";
        private const string GaugeNameRearLeft = "pitwall_tyres_wear_rearleft_percent";
        private const string GaugeNameFrontRight = "pitwall_tyres_wear_frontright_percent";
        private const string GaugeNameRearRight = "pitwall_tyres_wear_rearright_percent";

        readonly string[] tyreLabels = new[] { "Pilot" };

        private readonly Gauge _gaugeFrontLeft;
        private readonly Gauge _gaugeRearLeft;
        private readonly Gauge _gaugeFrontRight;
        private readonly Gauge _gaugeRearRight;

        public TyreWearRepository()
        {
            var configTyres = new GaugeConfiguration();
            configTyres.LabelNames = tyreLabels;

            _gaugeFrontLeft = Metrics.CreateGauge(GaugeNameFrontLeft, "Tyres wear front left in percent.", configTyres);
            _gaugeRearLeft = Metrics.CreateGauge(GaugeNameRearLeft, "Tyres wear front left in percent.", configTyres);
            _gaugeFrontRight = Metrics.CreateGauge(GaugeNameFrontRight, "Tyres wear front left in percent.", configTyres);
            _gaugeRearRight = Metrics.CreateGauge(GaugeNameRearRight, "Tyres wear front left in percent.", configTyres);
        }

        public string SerieName => GaugeNamePitwallTyreWearPercent;

        public string[] Labels => tyreLabels;

        public string Description => "Current tyre wear as percent.";

        public void UpdateFrontLeft(string pilotName, double? frontLeftWear)
        {
            UpdateGauge(frontLeftWear, pilotName, _gaugeFrontLeft);
        }

        public void UpdateFrontRight(string pilotName, double? frontRightWear)
        {
            UpdateGauge(frontRightWear, pilotName, _gaugeFrontRight);
        }

        public void UpdateRearLeft(string pilotName, double? reartLeftWear)
        {
            UpdateGauge(reartLeftWear, pilotName, _gaugeRearLeft);
        }

        public void UpdateRearRight(string pilotName, double? rearRightWear)
        {
            UpdateGauge(rearRightWear, pilotName, _gaugeRearRight);
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
