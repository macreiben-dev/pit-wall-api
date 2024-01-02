using PitWallDataGatheringApi.Models.Apis.v1;
using PitWallDataGatheringApi.Repositories;

namespace PitWallDataGatheringApi.Services
{
    public sealed class AuthenticatePayloadService : IAuthenticatePayloadService
    {
        private ISimerKeyRepository _simerKeys;

        public AuthenticatePayloadService(ISimerKeyRepository simerKeyReposity)
        {
            _simerKeys = simerKeyReposity;
        }

        public IEnumerable<string> ValidatePayload(ICallerInfos telemetry)
        {
            /**
             * Should add a mecanism to block unauthorized attempt for some time.
             * */


            if (telemetry.SimerKey != _simerKeys.Key)
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
