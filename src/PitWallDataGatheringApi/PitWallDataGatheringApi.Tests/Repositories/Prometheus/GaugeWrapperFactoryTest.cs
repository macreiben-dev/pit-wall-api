using NFluent;
using PitWallDataGatheringApi.Repositories.Prometheus;

namespace PitWallDataGatheringApi.Tests.Repositories.Prometheus
{
    public class GaugeWrapperFactoryTest
    {
        [Fact]
        public void THEN_create_gauge()
        {
            Check.ThatCode(() => new GaugeWrapperFactory().Create(
                "some_serie",
                "some description",
                new[] { "label1, label2" }
                ));
        }
    }
}
