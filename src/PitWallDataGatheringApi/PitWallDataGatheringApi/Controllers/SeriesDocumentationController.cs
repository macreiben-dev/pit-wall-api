using Microsoft.AspNetCore.Mvc;
using PitWallDataGatheringApi.Repositories.Prom;

namespace PitWallDataGatheringApi.Controllers
{
    [Route("api/v1/seriesdocumentation")]
    [ApiController]
    public class SeriesDocumentationController : ControllerBase
    {
        private IGaugeWrapperFactory _gaugeFactory;

        public SeriesDocumentationController(
            IGaugeWrapperFactory gaugeFactory)
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
