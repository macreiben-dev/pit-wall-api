using Microsoft.AspNetCore.Mvc;
using PitWallDataGatheringApi.Repositories.Gauges;

namespace PitWallDataGatheringApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SeriesDocumentationController : ControllerBase
    {
        private IGaugeFactory _gaugeFactory;

        public SeriesDocumentationController(
            IGaugeFactory gaugeFactory)
        {
            _gaugeFactory = gaugeFactory;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_gaugeFactory.ListCreated());
        }
    }
}
