using Microsoft.AspNetCore.Mvc;
using PitWallDataGatheringApi.Models.Apis;
using Prometheus;

namespace PitWallDataGatheringApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelemetryController : ControllerBase
    {
        private readonly Gauge _gaugeLapTimes;
        private readonly Gauge _gaugeTyre;
        readonly string[] tyreLabels = new[] { "TyreWear" };
        readonly string[] pilotLabels = new[] { "Pilot" };

        public TelemetryController()
        {
            // ---

            var configTyres = new GaugeConfiguration();
            configTyres.LabelNames = tyreLabels;
            _gaugeTyre = Metrics.CreateGauge("FuelAssistant_tyres_percent", "Tyres information.", configTyres);

            // ---

            var config = new GaugeConfiguration();
            config.LabelNames = pilotLabels;
            _gaugeLapTimes = Metrics.CreateGauge("FuelAssistant_laptimes_seconds", "Car laptimes in seconds.", config);
        }

        [HttpPost]
        public void Post(TelemetryModel telemetry)
        {
            /**
             * Move all this to a repository because it's only technical stuff.
             * 
             * */

            // ------

            if (telemetry != null && telemetry.Tyres != null)
            {
                var tyresWears = telemetry.Tyres;

                UpdateGauge(tyresWears.FrontLeftWear, "FrontLeft", _gaugeTyre);
                UpdateGauge(tyresWears.FrontLeftWear, "FrontRight", _gaugeTyre);
                UpdateGauge(tyresWears.FrontLeftWear, "RearLeft", _gaugeTyre);
                UpdateGauge(tyresWears.FrontLeftWear, "RearRight", _gaugeTyre);
            }
            // ------

            UpdateGauge(telemetry.LaptimeSeconds, telemetry.PilotName, _gaugeLapTimes);
            UpdateGauge(telemetry.LaptimeSeconds, "All", _gaugeLapTimes);
        }

        private void UpdateGauge(double? data, string gaugeLabel, Gauge gauge)
        {
            if (!data.HasValue) {
                return;
            }

            gauge.WithLabels(gaugeLabel).Set(data.Value);
        }
    }
}
