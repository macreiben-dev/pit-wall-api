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

        readonly string[] labels = new[] { "PilotName" };

        public TelemetryController()
        {
            var config = new GaugeConfiguration();
            config.LabelNames = new[] { "PilotName" };

            _gauge = Metrics.CreateGauge("FuelAssistant_laptime_milliseconds", "Car laptimes in milliseconds.", config);
        }

        [HttpPost]
        public void Post(TelemetryModel telemetry)
        {
            PromLapData sample = new PromLapData();

            sample.WithLaptimeMilliseconds(telemetry.LaptimeMilliseconds);

            _gauge.Set(sample.LaptimeMilliseconds);
        }
    }
}
