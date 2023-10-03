using Microsoft.AspNetCore.Mvc;
using PitWallDataGatheringApi.Models.Apis;
using PitWallDataGatheringApi.Services;
using Prometheus;

namespace PitWallDataGatheringApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelemetryController : ControllerBase
    {
        private readonly IPitwallTelemetryService pitwallTelemetryService;

        public TelemetryController(IPitwallTelemetryService pitwallTelemetryService)
        {
            this.pitwallTelemetryService = pitwallTelemetryService;
        }

        [HttpPost]
        public void Post(TelemetryModel telemetry)
        {
            Update(telemetry);
        }

        private void Update(TelemetryModel telemetry)
        {
            pitwallTelemetryService.Update(telemetry);
        }
    }
}
