using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PitWallDataGatheringApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        [SwaggerOperation(
            Summary = "Healthcheck endpoint",
            Description = "Always returns 200 OK. Used to check if the service is up and running.")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
