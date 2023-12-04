using ApiTelemetryModel = PitWallDataGatheringApi.Models.Apis.v1.TelemetryModel;
using PitWallDataGatheringApi.Services;
using NFluent;
using PitWallDataGatheringApi.Models.Business;

namespace PitWallDataGatheringApi.Tests.Services
{
    public partial class TelemetryModelMapperTest
    {
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

                source.TyresTemperatures = new PitWallDataGatheringApi.Models.Apis.TyresTemperatures();

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
    }
}
