using NSubstitute;
using PitWallDataGatheringApi.Repositories.Gauges.Prom;

namespace PitWallDataGatheringApi.Repositories.Tests;

public class MetricDataToGaugeTest
{
    [Fact]
    public void THEN_update_gauge()
    {
        var gauge = Substitute.For<IGauge>();
        var metric = new MetricData<double?>(
            1.0,
            new Models.PilotName("SomePilotName"),
            new Models.CarName("SomeCar"));

        MetricDataToGauge.Execute(gauge, metric);

        gauge.Received().Update(
            Arg.Is<string[]>(x => x.SequenceEqual(new[] {"SomePilotName", "SomeCar"})),
            1.0);
    }
}