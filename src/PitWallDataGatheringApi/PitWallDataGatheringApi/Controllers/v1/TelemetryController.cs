using Microsoft.AspNetCore.Mvc;
using PitWallDataGatheringApi.Models.Apis;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Services;

using IBusinessTelemetryModel = PitWallDataGatheringApi.Models.Business.ITelemetryModel;

namespace PitWallDataGatheringApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TelemetryController : ControllerBase
    {
        private readonly IPitwallTelemetryService _pitwallTelemetryService;
        private readonly ITelemetryModelMapper _mapper;
        private readonly ISimerKeyRepository _simerKeyRepository;

        public TelemetryController(
            IPitwallTelemetryService pitwallTelemetryService,
            ITelemetryModelMapper mapper,
            ISimerKeyRepository simerKeyRepository)
        {
            _pitwallTelemetryService = pitwallTelemetryService;
            _mapper = mapper;
            _simerKeyRepository = simerKeyRepository;
        }

        [HttpPost]
        public IActionResult Post(TelemetryModel telemetry)
        {
            if (telemetry.SimerKey != _simerKeyRepository.Key)
            {
                return Unauthorized();
            }

            var badRequestMessages = ValidatePayload(telemetry);

            if (badRequestMessages.Any())
            {
                return BadRequest(new ErrorMessages(telemetry, badRequestMessages));
            }

            IBusinessTelemetryModel mapped = _mapper.Map(telemetry);

            _pitwallTelemetryService.Update(mapped);

            return Ok();
        }

        private static IList<string> ValidatePayload(TelemetryModel telemetry)
        {
            IList<string> badRequestMessages = new List<string>();

            if (telemetry.PilotName == null)
            {
                badRequestMessages.Add("Pilot name is mandatory.");
            }

            if (telemetry.CarName == null)
            {
                badRequestMessages.Add("Car name is mandatory.");
            }

            return badRequestMessages;
        }
    }
}
