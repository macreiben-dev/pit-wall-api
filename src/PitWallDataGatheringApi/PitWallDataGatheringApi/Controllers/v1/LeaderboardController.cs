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
        private ILeaderboardModelMapper _mapper;
        private IAuthenticatePayloadService _authenticatePayload;

        public LeaderboardController(
            ILeaderboardModelMapper mapper,
            IAuthenticatePayloadService authenticatePayload)
        {
            _mapper = mapper;

            _authenticatePayload = authenticatePayload;
        }

        [HttpPost]
        public ActionResult Post(LeaderboardModel model)
        {
            try
            {
                var badRequestMessages = _authenticatePayload.ValidatePayload(model);

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
    }
}
