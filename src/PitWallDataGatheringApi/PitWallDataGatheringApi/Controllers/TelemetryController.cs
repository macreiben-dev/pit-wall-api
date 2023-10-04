using Microsoft.AspNetCore.Mvc;
using PitWallDataGatheringApi.Models.Apis;
using PitWallDataGatheringApi.Services;

using IBusinessTelemetryModel = PitWallDataGatheringApi.Models.Business.ITelemetryModel;

namespace PitWallDataGatheringApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelemetryController : ControllerBase
    {
        private readonly IPitwallTelemetryService _pitwallTelemetryService;
        private readonly ITelemetryModelMapper _mapper;

        public TelemetryController(
            IPitwallTelemetryService pitwallTelemetryService, 
            ITelemetryModelMapper mapper)
        {
            _pitwallTelemetryService = pitwallTelemetryService;
            _mapper = mapper;
        }

        [HttpPost]
        public void Post(TelemetryModel telemetry)
        {
            IBusinessTelemetryModel mapped = _mapper.Map(telemetry);

            _pitwallTelemetryService.Update(mapped);
        }
    }
}
