using PitWallDataGatheringApi.Models.Apis.v1;

namespace PitWallDataGatheringApi.Controllers
{
    public class ErrorMessages
    {
        public ErrorMessages(ICallerInfos original, IEnumerable<string> messages)
        {
            Errors = messages;
            Source = original;
        }

        public IEnumerable<string> Errors { get; }
        public ICallerInfos Source { get; }
    }
}
