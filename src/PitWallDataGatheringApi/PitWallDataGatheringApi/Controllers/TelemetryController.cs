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
            _gaugeTyre = Metrics.CreateGauge("FuelAssistant_tyres", "Tyres information.", configTyres);

            // ---

            var config = new GaugeConfiguration();
            config.LabelNames = pilotLabels;
            _gaugeLapTimes = Metrics.CreateGauge("FuelAssistant_laptimes", "Car laptimes in milliseconds.", config);
        }

        [HttpPost]
        public void Post(TelemetryModel telemetry)
        {
            // ------

            _gaugeTyre.WithLabels("FrontLeft").Set(telemetry.Tyres.FrontLeftWear);

            _gaugeTyre.WithLabels("FrontRight").Set(telemetry.Tyres.FrontRightWear);

            _gaugeTyre.WithLabels("RearLeft").Set(telemetry.Tyres.ReartLeftWear);

            _gaugeTyre.WithLabels("RearRight").Set(telemetry.Tyres.RearRightWear);

            // ------

            _gaugeLapTimes.WithLabels(telemetry.PilotName).Set(telemetry.LaptimeMilliseconds);
            _gaugeLapTimes.WithLabels("All").Set(telemetry.LaptimeMilliseconds);
        }
    }
}
