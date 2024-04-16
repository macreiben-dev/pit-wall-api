using NSubstitute;
using PitWallDataGatheringApi.Repositories.Gauges;
using PitWallDataGatheringApi.Repositories.Gauges.Prom;
using PitWallDataGatheringApi.Repositories.Leaderboards;

namespace PitWallDataGatheringApi.Repositories.Tests.Leaderboards;

public class LeaderboardInPitBoxRepositoryTest
{
    private readonly IGaugeFactory _gaugeFactory;
    private readonly IGauge _gauge;
    private readonly IEnumerable<string> _labels = new[] { "Pilot", "Car" };

    public LeaderboardInPitBoxRepositoryTest()
    {
        _gaugeFactory = Substitute.For<IGaugeFactory>();

        _gauge = Substitute.For<IGauge>();

        _gaugeFactory.Create(
            "pitwall_leaderboard_isinpitbox",
            "Leaderboard 'is in pitbox flag'.",
            Arg.Is<string[]>(args => args.SequenceEqual(_labels))).Returns(_gauge);
    }

    private LeaderboardInPitBoxRepository GetTarget()
    {
        return new LeaderboardInPitBoxRepository(_gaugeFactory);
    }
        
    [Fact]
    public void THEN_update_gauge()
    {
        var target = GetTarget();
        var metric = new MetricData<double?>(
            1.0,
            new Models.PilotName("SomePilotName"),
            new Models.CarName("SomeCar"));

        target.Update(metric);

        _gauge.Received().Update(
            Arg.Is<string[]>(x => x.SequenceEqual(new[] {"SomePilotName", "SomeCar"})),
            1.0);
    }
}