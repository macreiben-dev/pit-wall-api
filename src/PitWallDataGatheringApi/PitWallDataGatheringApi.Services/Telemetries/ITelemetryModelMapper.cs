using PitWallDataGatheringApi.Models.Business;

namespace PitWallDataGatheringApi.Services.Telemetries
{
    public interface ITelemetryModelMapper
    {
        ITelemetryModel Map(Models.Apis.v1.TelemetryModel apiModel);
    }
}