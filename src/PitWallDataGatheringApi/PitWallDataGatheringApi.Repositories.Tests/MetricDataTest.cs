using NFluent;
using PitWallDataGatheringApi.Models;

namespace PitWallDataGatheringApi.Repositories.Tests
{
    public class MetricDataTest
    {
        private const string CarName = "SomeCar";
        private const string PilotName = "SomePilotName";
        private readonly double? Data = 1.0;
        private MetricData<double?> _target;

        public MetricDataTest()
        {

            _target = new MetricData<double?>(
                Data, 
                new Models.CarName(CarName), 
                new Models.PilotName(PilotName));
        }

        [Fact]
        public void THEN_map_carName()
        {
            Check.That(_target.CarName.ToString()).IsEqualTo(CarName);
        }

        [Fact]
        public void THEN_map_PilotName()
        {
            Check.That(_target.PilotName.ToString()).IsEqualTo(PilotName);
        }

        [Fact]
        public void THEN_map_Data()
        {
            Check.That(_target.Data).IsEqualTo(Data);
        }

        [Fact]
        public void GIVEN_carName_pilotName_data_equal_THEN_equal()
        {
            var comparee = new MetricData<double?>(
                DefaultData(),
                DefaultCarName(),
                DefaultPilotName());

            var actual = _target.Equals(comparee);

            Check.That(actual).IsTrue();
        }

        [Fact]
        public void GIVEN_carName_different_THEN_notEqual()
        {
            var comparee = new MetricData<double?>(
                DefaultData(),
                new Models.CarName("OtherCarName"),
                DefaultPilotName());

            var actual = _target.Equals(comparee);

            Check.That(actual).IsFalse();
        }

        [Fact]
        public void GIVEN_PilotName_different_THEN_notEqual()
        {
            var comparee = new MetricData<double?>(
                DefaultData(),
                DefaultCarName(),
                new PilotName("SomeOtherPilotName"));

            var actual = _target.Equals(comparee);

            Check.That(actual).IsFalse();
        }

        private static PilotName DefaultPilotName()
        {
            return new Models.PilotName(PilotName);
        }

        private static CarName DefaultCarName()
        {
            return new Models.CarName(CarName);
        }

        private double? DefaultData() => Data;
    }
}
