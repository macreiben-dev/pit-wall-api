using Microsoft.AspNetCore.Mvc;
using PitWallDataGatheringApi.Models.Apis.v1;
using PitWallDataGatheringApi.Models.Apis.v1.Leaderboards;
using PitWallDataGatheringApi.Services;
using PitWallDataGatheringApi.Services.Leaderboards;
using Swashbuckle.AspNetCore.Annotations;

namespace PitWallDataGatheringApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LeaderboardController(
        ILeaderboardModelMapper mapper,
        IAuthenticatePayloadService authenticatePayload,
        ILeaderBoardService service)
        : ControllerBase
    {
        [HttpPost]
        [SwaggerOperation(
            Summary = "Post a new leaderboard metric",
            Description = "Post a new leaderboard metric, require a valid SimerKey")]
        [SwaggerResponse(401, "SimerKey is invalid.", typeof(ErrorMessages))]
        [SwaggerResponse(400, "Sent payload is invalid.", typeof(string))]
        public ActionResult Post(LeaderboardModel model)
        {
            /**
             * Does nothing anymore, left for compatibility reason with the old API
             *
             * To be removed in v2.
             */

            return Ok();
        }
    }
}
