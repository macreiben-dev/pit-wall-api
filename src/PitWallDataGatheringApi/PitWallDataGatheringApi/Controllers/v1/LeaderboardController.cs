using Microsoft.AspNetCore.Mvc;
using PitWallDataGatheringApi.Models.Apis.v1;
using PitWallDataGatheringApi.Models.Apis.v1.Leaderboards;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Services;

namespace PitWallDataGatheringApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private ISimerKeyRepository _simerKeys;
        private ILeaderboardModelMapper _mapper;

        public LeaderboardController(
            ISimerKeyRepository simerKeyReposity, 
            ILeaderboardModelMapper mapper)
        {
            _simerKeys = simerKeyReposity;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult Post(LeaderboardModel model)
        {
            try
            {
                var badRequestMessages = ValidatePayload(model, _simerKeys);

                if (badRequestMessages.Any())
                {
                    return BadRequest(new ErrorMessages(model, badRequestMessages));
                }
            }
            catch (PostMetricDeniedException ex)
            {
                return Unauthorized(ex.Message);
            }


            return Ok();
        }

        private static IList<string> ValidatePayload(ICallerInfos telemetry, ISimerKeyRepository simerKeyRepository)
        {
            /**
             * Should add a mecanism to block unauthorized attempt for some time.
             * */

            if (telemetry.SimerKey != simerKeyRepository.Key)
            {
                throw new PostMetricDeniedException(telemetry);
            }

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
