using PitWallDataGatheringApi.Models.Business;
using Prometheus;

namespace PitWallDataGatheringApi.Repositories.Tyres
{
    public sealed class TyresTemperaturesRepository
        : ITyresTemperaturesRepository, IDocumentationTyresTemperaturesSerie
    {
        private const string GaugeName = "pitwall_tyres_temperatures_celsius";
        
        private const string GaugeNameFrontLeft = "pitwall_tyres_temperatures_frontleft_celsius";
        private const string GaugeNameRearLeft = "pitwall_tyres_temperatures_rearleft_celsius";
        private const string GaugeNameFrontRight = "pitwall_tyres_temperatures_frontright_celsius";
        private const string GaugeNameRearRight = "pitwall_tyres_temperatures_rearright_celsius";

        private const string GaugeLabelFrontLeft = "FrontLeft";
        private const string GaugeLabelFrontRight = "FrontRight";
        private const string GaugeLabelRearLeft = "RearLeft";
        private const string GaugeLabelRearRight = "RearRight";

        readonly string[] tyreLabels = new[] { "Pilot" };

        private readonly Gauge _gaugeFrontLeftTyre;
        private readonly Gauge _gaugeRearLeftTyre;
        private readonly Gauge _gaugeFrontRightTyre;
        private readonly Gauge _gaugeRearRightTyre;

        public TyresTemperaturesRepository()
        {
            var configTyres = new GaugeConfiguration();

            configTyres.LabelNames = tyreLabels;
            
            _gaugeFrontLeftTyre = Metrics.CreateGauge(GaugeNameFrontLeft, "Front left tyre temperature in celsuis.", configTyres);
            _gaugeRearLeftTyre = Metrics.CreateGauge(GaugeNameRearLeft, "Front left tyre temperature in celsuis.", configTyres);
            _gaugeFrontRightTyre = Metrics.CreateGauge(GaugeNameFrontRight, "Front left tyre temperature in celsuis.", configTyres);
            _gaugeRearRightTyre = Metrics.CreateGauge(GaugeNameRearRight, "Front left tyre temperature in celsuis.", configTyres);
        }

        public string SerieName => GaugeName;

        public string[] Labels => tyreLabels;

        public string Description => "Current tyre temperature in celsius.";

        public void UpdateFrontLeft(ITyresTemperatures? data, string pilotName)
        {
            UpdateGauge(data.FrontLeftTemp, GaugeLabelFrontLeft, _gaugeFrontLeftTyre, pilotName);
            UpdateGauge(data.FrontLeftTemp, GaugeLabelFrontLeft, _gaugeFrontLeftTyre, "All");
        }

        public void UpdateFrontRight(ITyresTemperatures? data, string pilotName)
        {
            UpdateGauge(data.FrontRightTemp, GaugeLabelFrontRight, _gaugeFrontRightTyre, pilotName);
            UpdateGauge(data.FrontRightTemp, GaugeLabelFrontRight, _gaugeFrontRightTyre, "All");
        }

        public void UpdateRearLeft(ITyresTemperatures? data, string pilotName)
        {
            UpdateGauge(data.RearLeftTemp, GaugeLabelRearLeft, _gaugeRearLeftTyre, pilotName);
            UpdateGauge(data.RearLeftTemp, GaugeLabelRearLeft, _gaugeRearLeftTyre, "All");
        }

        public void UpdateRearRight(ITyresTemperatures? data, string pilotName)
        {
            UpdateGauge(data.RearRightTemp, GaugeLabelRearRight, _gaugeRearRightTyre, pilotName);
            UpdateGauge(data.RearRightTemp, GaugeLabelRearRight, _gaugeRearRightTyre, "All");
        }

        private void UpdateGauge(double? data, string gaugeLabel, Gauge gauge, string pilotName)
        {
            if (!data.HasValue)
            {
                return;
            }

            gauge.WithLabels(gaugeLabel).Set(data.Value);
        }
    }
}
