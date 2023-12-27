using ApiTelemetryModel = PitWallDataGatheringApi.Models.Apis.v1.TelemetryModel;
using NFluent;
using PitWallDataGatheringApi.Services.Telemetries;

namespace PitWallDataGatheringApi.Tests.Services
{
    public partial class TelemetryModelMapperTest
    {
        public class TyreWearTest
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
        }
    }
}
