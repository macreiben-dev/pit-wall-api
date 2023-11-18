namespace PitWallDataGatheringApi.Integration.Tests
{
    public partial class InstantMetricReadTest
    {
        // =============

        public class WeatherConsitionsTests
        {

            [Fact]
            public void GIVEN_pitwall_road_wetness_avg_percent_THEN_read_from_timeSerie()
            {
                var context = new Context(SimerKey)
                {
                    MetricName = "pitwall_road_wetness_avg_percent",
                    PilotName = "IntegrationTest_roadWetness",
                    SetFieldValue = t => t.AvgWetness = 0.3,
                    GetApiModelInstance = () => ModelWithoutSubMappings(),
                    Expected = 0.3
                };
                InstantMetricReadTestMain.
                            GIVEN_metric_THEN_read_from_timeSerie(context, TargetApi, TimeSerieUri);
            }

            [Fact]
            public void GIVEN_pitwall_laptimes_seconds_THEN_read_from_timeSerie()
            {
                var context = new Context(SimerKey)
                {
                    MetricName = "pitwall_laptimes_seconds",
                    PilotName = "IntegrationTest_laptime",
                    SetFieldValue = t => t.LaptimeSeconds = 122.0,
                    GetApiModelInstance = () => ModelWithoutSubMappings(),
                    Expected = 122.0
                };
                InstantMetricReadTestMain.
                            GIVEN_metric_THEN_read_from_timeSerie(context, TargetApi, TimeSerieUri);
            }

            [Fact]
            public void GIVEN_pitwall_air_temperature_celsius_THEN_read_from_timeSerie()
            {
                var context = new Context(SimerKey)
                {
                    MetricName = "pitwall_air_temperature_celsius",
                    PilotName = "IntegrationTest_airTemp",
                    SetFieldValue = t => t.AirTemperature = 10.0,
                    GetApiModelInstance = () => ModelWithoutSubMappings(),
                    Expected = 10.0
                };
                InstantMetricReadTestMain.
                            GIVEN_metric_THEN_read_from_timeSerie(context, TargetApi, TimeSerieUri);
            }

            [Fact]
            public void GIVEN_pitwall_track_temperature_celsius_THEN_read_from_timeSerie()
            {
                var context = new Context(SimerKey)
                {
                    MetricName = "pitwall_track_temperature_celsius",
                    PilotName = "IntegrationTest_trackTemp",
                    SetFieldValue = t => t.TrackTemperature = 33.3,
                    GetApiModelInstance = () => ModelWithoutSubMappings(),
                    Expected = 33.3
                };
                InstantMetricReadTestMain.
                           GIVEN_metric_THEN_read_from_timeSerie(context, TargetApi, TimeSerieUri);
            }
        }
    }
}