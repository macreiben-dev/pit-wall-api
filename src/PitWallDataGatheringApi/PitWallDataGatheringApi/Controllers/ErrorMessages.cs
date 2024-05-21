using PitWallDataGatheringApi.Models.Apis.v1;
using Swashbuckle.AspNetCore.Annotations;

namespace PitWallDataGatheringApi.Controllers
{
    [SwaggerSchema("The error context.")]
    public class ErrorMessages
    {
        public ErrorMessages(ICallerInfos original, IEnumerable<string> messages)
        {
            Errors = messages;
            Source = original;
        }

        [SwaggerSchema("The error messages.")]
        public IEnumerable<string> Errors { get; }
        [SwaggerSchema("The caller informations.")]
        public ICallerInfos Source { get; }
    }
}
