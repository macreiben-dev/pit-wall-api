using NFluent;
using NSubstitute;
using PitWallDataGatheringApi.Repositories.Gauges;
using PitWallDataGatheringApi.Repositories.Gauges.Prom;
using PitWallDataGatheringApi.Repositories.Leaderboards;

namespace PitWallDataGatheringApi.Repositories.Tests.Leaderboards;

public class LeaderboardInPitBoxRepositoryTest
{
    private readonly IGaugeFactory _gaugeFactory;
    private readonly IGauge _gauge;
    
    private const string MetricCar = "SomeCar";
    private const string MetricPilot = "SomePilot";
    private const string SourceMetricCar = "SourceMetricCar";
    private const string SourceMetricPilot = "SourceMetricPilot";
    
    readonly ISet<string> _expectedLabel = new HashSet<string>()
    {
        "Pilot",
        "Car",
        "SourcePilot",
        "SourceCar"
    };
    
    public LeaderboardInPitBoxRepositoryTest()
    {
        _gaugeFactory = Substitute.For<IGaugeFactory>();

        _gauge = Substitute.For<IGauge>();

        _gaugeFactory.Create(
            "pitwall_leaderboard_isinpitbox",
            "Leaderboard 'is in pitbox flag'.",
            Arg.Any<IEnumerable<string>>()).Returns(_gauge);
    }

    private LeaderboardInPitBoxRepository GetTarget()
    {
        return new LeaderboardInPitBoxRepository(_gaugeFactory);
    }
        
    
    [Fact]
    public void THEN_gauge_expected_label_set()
    {
        var gaugeFactory = Substitute.For<IGaugeFactory>();

        var actualLabels = Enumerable.Empty<string>();
            
        gaugeFactory.Create("pitwall_leaderboard_isinpitbox", 
                "Leaderboard 'is in pitbox flag'.", 
                Arg.Do<IEnumerable<string>>(
                    arg => actualLabels = arg))
            .Returns(_gauge);

        _ = new LeaderboardInPitBoxRepository(gaugeFactory);

        Check.That(actualLabels).ContainsExactly(_expectedLabel);
    }
    
    [Fact]
    public void THEN_update_gauge()
    {
        var target = GetTarget();
        double? originalValue = 1.0;
        
        var metric = new MetricDataWithSource<double?>(
            originalValue,
            new Models.PilotName(MetricPilot),
            new Models.CarName(MetricCar),
            new Models.PilotName(SourceMetricPilot),
            new Models.CarName(SourceMetricCar));

        ISet<string> expectedLabels = new HashSet<string>()
        {
            MetricPilot,
            MetricCar,
            SourceMetricPilot,
            SourceMetricCar
        };
        
        var actualLabels = Enumerable.Empty<string>();
       
        _gauge.Update(
            Arg.Do<IEnumerable<string>>(
                arg => actualLabels = arg),
            1.0);
        
        target.Update(metric);

        Check.That(actualLabels).ContainsExactly(expectedLabels);
    }
}