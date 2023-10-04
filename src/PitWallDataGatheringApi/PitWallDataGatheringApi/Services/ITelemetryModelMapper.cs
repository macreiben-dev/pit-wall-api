using PitWallDataGatheringApi.Models.Business;

namespace PitWallDataGatheringApi.Services
{
    public interface ITelemetryModelMapper
    {
        ITelemetryModel Map(Models.Apis.TelemetryModel apiModel);
    }
}