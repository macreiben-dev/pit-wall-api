using PitWallDataGatheringApi.Models.Apis;

namespace PitWallDataGatheringApi.Controllers
{
    public class ErrorMessages
    {
        public ErrorMessages(TelemetryModel original, IEnumerable<string> messages)
        {
            Errors = messages;
            Source = original;
        }

        public IEnumerable<string> Errors { get; }
        public TelemetryModel Source { get; }
    }
}
