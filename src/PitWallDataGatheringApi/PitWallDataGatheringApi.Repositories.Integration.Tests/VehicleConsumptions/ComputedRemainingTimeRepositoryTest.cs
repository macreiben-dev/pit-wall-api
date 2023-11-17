using PitWallDataGatheringApi.Repositories.Prom;
using PitWallDataGatheringApi.Repositories.VehicleConsumptions;
using Prometheus;

namespace PitWallDataGatheringApi.Repositories.Integration.Tests.VehicleConsumptions
{
    public class ComputedRemainingTimeRepositoryTest
    {
        [Fact]
        public void THEN_sendToProm()
        {
            ComputedRemainingTimeRepository target = new ComputedRemainingTimeRepository(new GaugeWrapperFactory());

            target.Update(13.0, "SomePilot", new Models.CarName("32"));

        }
    }
}
