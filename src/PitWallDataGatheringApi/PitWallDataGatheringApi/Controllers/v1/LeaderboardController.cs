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
            try
            {
                var badRequestMessages = authenticatePayload.ValidatePayload(model);

                var requestMessages = badRequestMessages as string[] ?? badRequestMessages.ToArray();
                
                if (requestMessages.Length != 0)
                {
                    return BadRequest(new ErrorMessages(model, requestMessages));
                }
            }
            catch (PostMetricDeniedException ex)
            {
                return Unauthorized(ex.Message);
            }

            var actual = mapper.Map(model);

            service.Update(actual);

            return Ok();
        }
    }
}
