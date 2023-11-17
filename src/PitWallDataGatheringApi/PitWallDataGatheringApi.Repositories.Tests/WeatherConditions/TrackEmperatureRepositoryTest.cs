using NFluent;
using NSubstitute;
using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Prom;
using PitWallDataGatheringApi.Repositories.WeatherConditions;

namespace PitWallDataGatheringApi.Tests.Repositories.WeatherConditions
{
    public class TrackEmperatureRepositoryTest
    {
        private IGaugeWrapperFactory _gaugeFactory;
        private IGauge _gaugeWrapper;

        public TrackEmperatureRepositoryTest()
        {

            _gaugeFactory = Substitute.For<IGaugeWrapperFactory>();

            _gaugeWrapper = Substitute.For<IGauge>();

            _gaugeFactory.Create(
                "pitwall_track_temperature_celsius",
                "Track temperature in celsius",
                Arg.Any<string[]>()
                ).Returns(_gaugeWrapper);
        }
        private TrackEmperatureRepository GetTarget()
        {
            return new TrackEmperatureRepository(_gaugeFactory);
        }

        [Fact]
        public void GIVEN_instance_THEN_update_track_temp_for_pilot()
        {
            var originalLabels = new string[] { "thePilotName01", "32" };

            var actualLabels = new string[0];
            double? actualValue = 0.0;

            _gaugeWrapper.When(c => c.Update(
                 Arg.Any<string[]>(),
                 Arg.Any<double?>()))
                     .Do(callback =>
                     {
                         actualLabels = (string[])callback.Args()[0];
                         actualValue = (double?)callback.Args()[1];
                     });

            // ACT
            var target = GetTarget();

            target.Update(13.3, "thePilotName01", new CarName("32"));

            // ASSERT
            Check.That(actualLabels).ContainsExactly(originalLabels);
        }

        [Fact]
        public void GIVEN_instance_AND_noCarName_THEN_useDefault_car_number()
        {
            var originalLabels = new string[] { "thePilotName01", "NoCarNumber" };

            var actualLabels = new string[0];
            double? actualValue = 0.0;

            _gaugeWrapper.When(c => c.Update(
                 Arg.Any<string[]>(),
                 Arg.Any<double?>()))
                     .Do(callback =>
                     {
                         actualLabels = (string[])callback.Args()[0];
                         actualValue = (double?)callback.Args()[1];
                     });

            // ACT
            var target = GetTarget();

            target.Update(13.3, "thePilotName01", new CarName(null));

            // ASSERT
            Check.That(actualLabels).ContainsExactly(originalLabels);
        }
    }
}
