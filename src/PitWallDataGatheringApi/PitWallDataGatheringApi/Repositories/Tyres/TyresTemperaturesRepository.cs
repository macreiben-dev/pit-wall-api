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
            UpdateGauge(data.FrontLeftTemp, pilotName, _gaugeFrontLeftTyre);
            UpdateGauge(data.FrontLeftTemp, "All", _gaugeFrontLeftTyre);
        }

        public void UpdateFrontRight(ITyresTemperatures? data, string pilotName)
        {
            UpdateGauge(data.FrontRightTemp, pilotName, _gaugeFrontRightTyre);
            UpdateGauge(data.FrontRightTemp, "All", _gaugeFrontRightTyre);
        }

        public void UpdateRearLeft(ITyresTemperatures? data, string pilotName)
        {
            UpdateGauge(data.RearLeftTemp, pilotName, _gaugeRearLeftTyre);
            UpdateGauge(data.RearLeftTemp, "All", _gaugeRearLeftTyre);
        }

        public void UpdateRearRight(ITyresTemperatures? data, string pilotName)
        {
            UpdateGauge(data.RearRightTemp, pilotName, _gaugeRearRightTyre);
            UpdateGauge(data.RearRightTemp, "All", _gaugeRearRightTyre);
        }

        private void UpdateGauge(double? data, string pilotName, Gauge gauge)
        {
            if (!data.HasValue)
            {
                return;
            }

            gauge.WithLabels(pilotName).Set(data.Value);
        }
    }
}
