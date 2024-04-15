using Microsoft.AspNetCore.Mvc;
using PitWallDataGatheringApi.Models.Apis.v1;
using PitWallDataGatheringApi.Models.Apis.v1.Leaderboards;
using PitWallDataGatheringApi.Repositories.Leaderboards.Reads;
using PitWallDataGatheringApi.Services;
using PitWallDataGatheringApi.Services.Leaderboards;

namespace PitWallDataGatheringApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LeaderboardController(
        ILeaderboardModelMapper mapper,
        IAuthenticatePayloadService authenticatePayload,
        ILeaderBoardService service,
        IReadLeaderboardRepository readLeaderboardRepository)
        : ControllerBase
    {
        [HttpPost]
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

        [HttpGet]
        public ActionResult<IEnumerable<ILeaderboardReadEntry>> Get(string pilotName, string carName)
        {
            return Ok(readLeaderboardRepository.Get(pilotName, carName));
        }
    }
}
