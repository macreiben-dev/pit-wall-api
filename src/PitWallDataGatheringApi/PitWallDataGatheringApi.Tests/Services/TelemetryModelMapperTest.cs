using ApiTelemetryModel = PitWallDataGatheringApi.Models.Apis.TelemetryModel;
using PitWallDataGatheringApi.Services;
using NFluent;
using PitWallDataGatheringApi.Models.Business;

namespace PitWallDataGatheringApi.Tests.Services
{
    public class TelemetryModelMapperTest
    {
        private TelemetryModelMapper GetTarget()
        {
            return new TelemetryModelMapper();
        }

        [Fact]
        public void GIVEN_tyresWear_is_notNull_THEN_mapFrontLeft()
        {
            ApiTelemetryModel source = new ApiTelemetryModel();

            double expected = 50.0;

            source.TyresWear.FrontLeftWear = expected;

            var target = GetTarget();

            var actual = target.Map(source);

            Check.That(actual.TyresWear.FrontLeftWear).IsEqualTo(expected);
        }

        [Fact]
        public void GIVEN_tyresWear_is_notNull_THEN_mapFrontRight()
        {
            ApiTelemetryModel source = new ApiTelemetryModel();

            double expected = 51.0;

            source.TyresWear.FrontRightWear = expected;

            var target = GetTarget();

            var actual = target.Map(source);

            Check.That(actual.TyresWear.FrontRightWear).IsEqualTo(expected);
        }

        [Fact]
        public void GIVEN_tyresWear_is_notNull_THEN_mapRearLeft()
        {
            ApiTelemetryModel source = new ApiTelemetryModel();

            double expected = 52.0;

            source.TyresWear.ReartLeftWear = expected;

            var target = GetTarget();

            var actual = target.Map(source);

            Check.That(actual.TyresWear.ReartLeftWear).IsEqualTo(expected);
        }

        [Fact]
        public void GIVEN_tyresWear_is_notNull_THEN_mapRearRight()
        {
            ApiTelemetryModel source = new ApiTelemetryModel();

            double expected = 53.0;

            source.TyresWear.RearRightWear = expected;

            var target = GetTarget();

            var actual = target.Map(source);

            Check.That(actual.TyresWear.RearRightWear).IsEqualTo(expected);
        }

        public class TyreTemperatureTest
        {
            private const double FrontLeftTemp = 10.0;
            private const double FrontRightTemp = 11.0;
            private const double RearRightTemp = 12.0;
            private const double RearLeftTemp = 13.0;

            private ITyresTemperatures _actual;

            public TyreTemperatureTest()
            {
                ApiTelemetryModel source = new ApiTelemetryModel();

                source.TyresTemperatures = new Models.Apis.TyresTemperatures();

                source.TyresTemperatures.FrontLeftTemp = FrontLeftTemp;
                source.TyresTemperatures.FrontRightTemp = FrontRightTemp;
                source.TyresTemperatures.RearLeftTemp = RearLeftTemp;
                source.TyresTemperatures.RearRightTemp = RearRightTemp;

                var target = GetTarget();

                var actual = target.Map(source);

                _actual = actual.TyresTemperatures;
            }

            private TelemetryModelMapper GetTarget()
            {
                return new TelemetryModelMapper();
            }

            [Fact]
            public void Should_map_frontLeftTemp()
            {
                Check.That(_actual.FrontLeftTemp).IsEqualTo(FrontLeftTemp);
            }

            [Fact]
            public void Should_map_frontRightTemp()
            {
                Check.That(_actual.FrontRightTemp).IsEqualTo(FrontRightTemp);
            }

            [Fact]
            public void Should_map_rearLeftTemp()
            {
                Check.That(_actual.RearLeftTemp).IsEqualTo(RearLeftTemp);
            }

            [Fact]
            public void Should_map_rearRightTemp()
            {
                Check.That(_actual.RearRightTemp).IsEqualTo(RearRightTemp);
            }
        }

        public class WeatherTest
        {
            private const double AvgWetness = 10.0;
            private const double AirTemperature = 11.0;
            private ITelemetryModel? _actual;

            public WeatherTest()
            {
                ApiTelemetryModel source = new ApiTelemetryModel();

                source.AirTemperature = AirTemperature;
                source.AvgWetness = AvgWetness;

                _actual = GetTarget().Map(source);
            }

            private TelemetryModelMapper GetTarget()
            {
                return new TelemetryModelMapper();
            }


            [Fact]
            public void Should_map_avgWetness()
            {
                Check.That(_actual.AvgWetness).IsEqualTo(AvgWetness);
            }
        }
    }
}
