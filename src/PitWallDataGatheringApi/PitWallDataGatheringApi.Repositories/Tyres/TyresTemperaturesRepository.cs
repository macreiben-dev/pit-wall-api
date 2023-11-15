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

        /**
         * Idea : a lot of repeatition here.
         * */

        public void UpdateFrontLeft(string pilotName, double? frontLeftTemp)
        {
            UpdateGauge(frontLeftTemp, pilotName, _gaugeFrontLeftTyre);
            UpdateGauge(frontLeftTemp, "All", _gaugeFrontLeftTyre);
        }

        public void UpdateFrontRight(string pilotName, double? frontRightTemp)
        {
            UpdateGauge(frontRightTemp, pilotName, _gaugeFrontRightTyre);
            UpdateGauge(frontRightTemp, "All", _gaugeFrontRightTyre);
        }

        public void UpdateRearLeft(string pilotName, double? rearLeftTemp)
        {
            UpdateGauge(rearLeftTemp, pilotName, _gaugeRearLeftTyre);
            UpdateGauge(rearLeftTemp, "All", _gaugeRearLeftTyre);
        }

        public void UpdateRearRight(string pilotName, double? rearRightTemp)
        {
            UpdateGauge(rearRightTemp, pilotName, _gaugeRearRightTyre);
            UpdateGauge(rearRightTemp, "All", _gaugeRearRightTyre);
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
