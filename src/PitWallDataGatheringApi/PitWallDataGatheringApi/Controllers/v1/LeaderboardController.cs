using Microsoft.AspNetCore.Mvc;
using PitWallDataGatheringApi.Models.Apis.v1;

namespace PitWallDataGatheringApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        public LeaderboardController() { }

        [HttpPost]
        public ActionResult Post(LeaderboardModel model)
        {
            return Ok();
        }
    }
}
