using PitWallDataGatheringApi.Models.Business;
using Prometheus;

namespace PitWallDataGatheringApi.Repositories
{
    public sealed class TyreWearRepository : ITyreWearRepository
    {
        private const string GaugeNamePitwallTyreWearPercent = "pitwall_tyreswear_percent";
        private const string GaugeLabelFrontLeft = "FrontLeft";
        private const string GaugeLabelFrontRight = "FrontRight";
        private const string GaugeLabelRearLeft = "RearLeft";
        private const string GaugeLabelRearRight = "RearRight";
        
        readonly string[] tyreLabels = new[] { "TyreWear" };

        private readonly Gauge _gaugeTyre;

        public TyreWearRepository()
        {
            var configTyres = new GaugeConfiguration();
            configTyres.LabelNames = tyreLabels;
            _gaugeTyre = Metrics.CreateGauge(GaugeNamePitwallTyreWearPercent, "Tyres information.", configTyres);
        }

        public void UpdateFrontLeft(ITyresWear? tyresWears)
        {
            UpdateGauge(tyresWears.FrontLeftWear, GaugeLabelFrontLeft, _gaugeTyre);
        }

        public void UpdateFrontRight(ITyresWear? tyresWears)
        {
            UpdateGauge(tyresWears.FrontRightWear, GaugeLabelFrontRight, _gaugeTyre);
        }

        public void UpdateRearLeft(ITyresWear? tyresWears)
        {
            UpdateGauge(tyresWears.ReartLeftWear, GaugeLabelRearLeft, _gaugeTyre);
        }

        public void UpdateRearRight(ITyresWear? tyresWears)
        {
            UpdateGauge(tyresWears.RearRightWear, GaugeLabelRearRight, _gaugeTyre);
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
