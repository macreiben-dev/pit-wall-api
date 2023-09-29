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
        private readonly Gauge _gaugeFrontLeftTyre;
        private readonly Gauge _gaugeFrontRightTyre;
        private readonly Gauge _gaugeRearLeftTyre;
        private readonly Gauge _gaugeRearRightTyre;
        readonly string[] labels = new[] { "Pilot1", "Pilot2", "Pilot3", "Pilot4", "Pilot5", "All" };
        //readonly string[] tyreLabels = new[] { "FrontLeftWear", "FrontRightWear", "RearLeftWear", "RearRightWear" };
        readonly string[] tyreLabels = new[] { "TyreWear" };

        public TelemetryController()
        {
            var config = new GaugeConfiguration();
            config.LabelNames = new[] { "Pilot1", "All" };

            // ---

            var configTyres = new GaugeConfiguration();
            configTyres.LabelNames = tyreLabels;
            _gaugeTyre = Metrics.CreateGauge("FuelAssistant_tyres", "Tyres information.", configTyres);


            _gauge = Metrics.CreateGauge("FuelAssistant_laptime_milliseconds", "Car laptimes in milliseconds.", config);


            _gaugeFrontLeftTyre = Metrics.CreateGauge("FuelAssistant_tyreWear_front_left", "Tyre wear in percent for front left tyre.");
            _gaugeFrontRightTyre = Metrics.CreateGauge("FuelAssistant_tyreWear_front_right", "Tyre wear in percent for front right tyre.");
            _gaugeRearLeftTyre = Metrics.CreateGauge("FuelAssistant_tyreWear_rear_left", "Tyre wear in percent from rear left tyre.");
            _gaugeRearRightTyre = Metrics.CreateGauge("FuelAssistant_tyreWear_rear_right", "Tyre wear in percent from rear riht tyre.");
        }

        [HttpPost]
        public void Post(TelemetryModel telemetry)
        {
            PromLapData sample = new PromLapData();

            // ------

            sample.WithLaptimeMilliseconds(telemetry.LaptimeMilliseconds);

            sample.WithPilotLaptime(telemetry.PilotName, telemetry.LaptimeMilliseconds);

            // ------

            _gauge.Set(sample.LaptimeMilliseconds);

            Gauge.Child temp = _gaugeTyre.WithLabels("FrontLeft");

            temp.Set(0.5);
        }
    }
}
