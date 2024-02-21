using Microsoft.AspNetCore.Mvc;
using PitWallDataGatheringApi.Models.Apis.v1;
using PitWallDataGatheringApi.Models.Apis.v1.Leaderboards;
using PitWallDataGatheringApi.Repositories.Leaderboards;
using PitWallDataGatheringApi.Services;
using PitWallDataGatheringApi.Services.Leaderboards;

namespace PitWallDataGatheringApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private ILeaderboardModelMapper _mapper;
        private IAuthenticatePayloadService _authenticatePayload;
        private ILeaderBoardService _service;
        private IReadLeaderboardRepository _readLeaderboardRepository;

        public LeaderboardController(
            ILeaderboardModelMapper mapper,
            IAuthenticatePayloadService authenticatePayload,
            ILeaderBoardService service,
            IReadLeaderboardRepository readLeaderboardRepository)
        {
            _mapper = mapper;

            _authenticatePayload = authenticatePayload;

            _service = service;

            _readLeaderboardRepository = readLeaderboardRepository;
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

            var actual = _mapper.Map(model);

            _service.Update(actual);

            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<ILeaderboardReadEntry>> Get(string pilotName, string carName)
        {
            return Ok(_readLeaderboardRepository.Get(pilotName, carName));
        }
    }
}
