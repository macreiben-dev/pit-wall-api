using PitWallDataGatheringApi.Models.Apis.v1;

namespace PitWallDataGatheringApi.Services.Tests
{
    public class FakeCallerInfos : ICallerInfos
    {
        public string? PilotName { get; set; }

        public string? CarName { get; set; }

        public string SimerKey { get; set; }
    }
}
