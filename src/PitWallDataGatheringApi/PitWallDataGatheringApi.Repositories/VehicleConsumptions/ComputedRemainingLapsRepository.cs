using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Prom;

namespace PitWallDataGatheringApi.Repositories.VehicleConsumptions
{
    public sealed class ComputedRemainingLapsRepository : IComputedRemainingLapsRepository
    {
        private const string LocalSerieName = "pitwall_computed_remaininglap_volume";
        private readonly IGauge _gauge;

        private string Description => "Computed - remaining lap given current fuel volume.";


        public ComputedRemainingLapsRepository(IGaugeWrapperFactory _gaugeFactory)
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
