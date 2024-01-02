using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using PitWallDataGatheringApi.Models.Apis.v1;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Services;
using PitWallDataGatheringApi.Services.Telemetries;
using IBusinessTelemetryModel = PitWallDataGatheringApi.Models.Business.ITelemetryModel;

namespace PitWallDataGatheringApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TelemetryController : ControllerBase
    {
        private readonly IPitwallTelemetryService _pitwallTelemetryService;
        private readonly ITelemetryModelMapper _mapper;
        private readonly IAuthenticatePayloadService _authenticatePayloadService;

        public TelemetryController(
            IPitwallTelemetryService pitwallTelemetryService,
            ITelemetryModelMapper mapper,
            IAuthenticatePayloadService authenticatePayload)
        {
            _pitwallTelemetryService = pitwallTelemetryService;
            _mapper = mapper;

            _authenticatePayloadService = authenticatePayload;
        }

        [HttpPost]
        public IActionResult Post(TelemetryModel telemetry)
        {
            try
            {
                var badRequestMessages = _authenticatePayloadService.ValidatePayload(telemetry);

                if (badRequestMessages.Any())
                {
                    return BadRequest(new ErrorMessages(telemetry, badRequestMessages));
                }
            }
            catch (PostMetricDeniedException ex)
            {
                return Unauthorized(ex.Message);
            }

            IBusinessTelemetryModel mapped = _mapper.Map(telemetry);

            _pitwallTelemetryService.Update(mapped);

            return Ok();
        }
    }
}
