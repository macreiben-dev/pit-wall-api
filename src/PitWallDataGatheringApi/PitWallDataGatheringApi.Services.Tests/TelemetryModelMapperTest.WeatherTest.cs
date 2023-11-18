using ApiTelemetryModel = PitWallDataGatheringApi.Models.Apis.TelemetryModel;
using PitWallDataGatheringApi.Services;
using NFluent;
using PitWallDataGatheringApi.Models.Business;

namespace PitWallDataGatheringApi.Tests.Services
{
    public partial class TelemetryModelMapperTest
    {
        public class WeatherTest
        {
            private const double AvgWetness = 10.0;
            private const double AirTemperature = 11.0;
            private const double TrackTemperature = 12.0;

            private ITelemetryModel? _actual;

            public WeatherTest()
            {
                ApiTelemetryModel source = new ApiTelemetryModel();

                source.AirTemperature = AirTemperature;
                source.AvgWetness = AvgWetness;
                source.TrackTemperature = TrackTemperature;

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

            [Fact]
            public void Should_map_track_temperature()
            {
                Check.That(_actual.TrackTemperature).IsEqualTo(TrackTemperature);
            }
        }
    }
}
