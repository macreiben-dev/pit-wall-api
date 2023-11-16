using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Prometheus;

namespace PitWallDataGatheringApi.Repositories.VehicleConsumptions
{
    public sealed class ComputedRemainingTimeRepository : IComputedRemainingTimeRepository
    {
        private const string LocalSerieName = "pitwall_computed_remainingtimeonfuel_seconds";
        private readonly IGauge _gauge;

        private string Description => "Computed - remaining duration until fuel runs out.";

        public ComputedRemainingTimeRepository(IGaugeWrapperFactory _gaugeFactory)
        {
            _gauge = _gaugeFactory.Create(
              LocalSerieName,
              Description,
              ConstantLabels.Labels);
        }

        public void Update(double? data, string pilotName, CarName carName)
        {
            _gauge.Update(new[] { pilotName, carName.ToString() }, data);
        }
    }
}
