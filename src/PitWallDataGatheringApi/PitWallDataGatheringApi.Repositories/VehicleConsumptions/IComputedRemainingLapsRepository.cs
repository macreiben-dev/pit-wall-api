using PitWallDataGatheringApi.Models;

namespace PitWallDataGatheringApi.Repositories.VehicleConsumptions
{
    public interface IComputedRemainingLapsRepository : IMetricRepository, IMetricRepositoryV2<double?>
    {
    }
}