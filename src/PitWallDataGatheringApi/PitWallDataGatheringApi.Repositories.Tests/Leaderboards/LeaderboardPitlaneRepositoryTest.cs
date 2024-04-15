using NSubstitute;
using PitWallDataGatheringApi.Repositories.Gauges;
using PitWallDataGatheringApi.Repositories.Gauges.Prom;
using PitWallDataGatheringApi.Repositories.Leaderboards;

namespace PitWallDataGatheringApi.Repositories.Tests.Leaderboards
{
    public class LeaderboardPitlaneRepositoryTest
    {
        private readonly IGaugeFactory _gaugeFactory;
        private readonly IGauge _gauge;
        private readonly IEnumerable<string> _labels = new[] { "Pilot", "Car" };

        public LeaderboardPitlaneRepositoryTest()
        {
            _gaugeFactory = Substitute.For<IGaugeFactory>();

            _gauge = Substitute.For<IGauge>();

            _gaugeFactory.Create(
                "pitwall_leaderaboard_is_pitlane",
                "Leaderboard 'is in pitlane flag'.",
                Arg.Is<string[]>(args => args.SequenceEqual(_labels))).Returns(_gauge);
        }

        private LeaderboardPitlaneRepository GetTarget()
        {
            return new LeaderboardPitlaneRepository(_gaugeFactory);
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
}