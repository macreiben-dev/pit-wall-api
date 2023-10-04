using PitWallDataGatheringApi.Models.Business;
using Prometheus;

namespace PitWallDataGatheringApi.Repositories
{
    public sealed class TyresTemperaturesRepository 
        : ITyresTemperaturesRepository, IDocumentationTyresTemperaturesSerie
    {
        private const string GaugeName = "pitwall_tyres_temperatures_celsius";
        private const string GaugeLabelFrontLeft = "FrontLeft";
        private const string GaugeLabelFrontRight = "FrontRight";
        private const string GaugeLabelRearLeft = "RearLeft";
        private const string GaugeLabelRearRight = "RearRight";

        readonly string[] tyreLabels = new[] { "Tyre" };

        private readonly Gauge _gaugeTyre;

        public TyresTemperaturesRepository()
        {
            var configTyres = new GaugeConfiguration();
            configTyres.LabelNames = tyreLabels;
            _gaugeTyre = Metrics.CreateGauge(GaugeName, "Tyres information.", configTyres);
        }

        public string SerieName => GaugeName;

        public string[] Labels => tyreLabels;

        public string Description => "Current tyre temperature in celsius.";

        public void UpdateFrontLeft(ITyresTemperatures? data)
        {
            UpdateGauge(data.FrontLeftTemp, GaugeLabelFrontLeft, _gaugeTyre);
        }

        public void UpdateFrontRight(ITyresTemperatures? data)
        {
            UpdateGauge(data.FrontRightTemp, GaugeLabelFrontRight, _gaugeTyre);
        }

        public void UpdateRearLeft(ITyresTemperatures? data)
        {
            UpdateGauge(data.RearLeftTemp, GaugeLabelRearLeft, _gaugeTyre);
        }

        public void UpdateRearRight(ITyresTemperatures? data)
        {
            UpdateGauge(data.RearRightTemp, GaugeLabelRearRight, _gaugeTyre);
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
