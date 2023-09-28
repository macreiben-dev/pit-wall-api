using Microsoft.AspNetCore.Mvc;
using PitWallDataGatheringApi.Models.Apis;
using PitWallDataGatheringApi.Models.Prom;
using Prometheus;

namespace PitWallDataGatheringApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelemetryController : ControllerBase
    {
        private readonly Gauge _gauge;
        private readonly Gauge _gaugeTyre;
        
        readonly string[] labels = new[] { "Pilot1", "Pilot2", "Pilot3", "Pilot4", "Pilot5", "All" };
        readonly string[] tyreLabels = new[] { "FrontLeftWear", "FrontRightWear", "RearLeftWear", "RearRightWear" };

        public TelemetryController()
        {
            var config = new GaugeConfiguration();
            config.LabelNames = new[] { "Pilot1", "All" };

            var configTyres = new GaugeConfiguration();
            
            configTyres.LabelNames = tyreLabels;

            _gauge = Metrics.CreateGauge("FuelAssistant_laptime_milliseconds", "Car laptimes in milliseconds.", config);
            _gaugeTyre = Metrics.CreateGauge("FuelAssistant_tyreWear_front_left", "Tyre wear in percent for front left tyre.", configTyres);
        }

        [HttpPost]
        public void Post(TelemetryModel telemetry)
        {
            PromLapData sample = new PromLapData();

            sample.WithLaptimeMilliseconds(telemetry.LaptimeMilliseconds);
            // ------

            _gauge.WithLabels(new[] { "Pilot1", "All" }).Set(telemetry.LaptimeMilliseconds);

            _gauge.WithLabels(tyreLabels).Set(sample.LaptimeMilliseconds);

            // ------

            _gaugeTyre.WithLabels(new[] { "FrontLeftWear" }).Set(telemetry.Tyres.FrontLeftWear);
        }
    }
}
