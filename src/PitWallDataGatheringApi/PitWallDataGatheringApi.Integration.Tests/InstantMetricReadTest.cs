using PitWallDataGatheringApi.Models.Apis;

namespace PitWallDataGatheringApi.Integration.Tests
{
    public class InstantMetricReadTest
    {
        private const string TimeSerieUri = "http://localhost:10100";
        private const string TargetApi = "http://localhost:32773";
        private const string SimerKey = "some_test_looking_value23";

        public class TyreWearTests
        {
            [Fact]
            public void GIVEN_pitwall_tyres_wear_rearleft_percent_THEN_read_from_timeSerie()
            {
                var context = new Context(SimerKey)
                {
                    MetricName = "pitwall_tyres_wear_rearleft_percent",
                    PilotName = "IntegrationTest_twear_rearleft",
                    SetFieldValue = t => t.TyresWear.ReartLeftWear = 60.0,
                    GetApiModelInstance = () => ModelWithTyreWear(),
                    Expected = 60.0
                };
                InstantMetricReadTestMain.
                            GIVEN_metric_THEN_read_from_timeSerie(context, TargetApi, TimeSerieUri);
            }

            [Fact]
            public void GIVEN_pitwall_tyres_wear_rearright_percent_THEN_read_from_timeSerie()
            {
                var context = new Context(SimerKey)
                {
                    MetricName = "pitwall_tyres_wear_rearright_percent",
                    PilotName = "IntegrationTest_twear_rearright",
                    SetFieldValue = t => t.TyresWear.RearRightWear = 61.0,
                    GetApiModelInstance = () => ModelWithTyreWear(),
                    Expected = 61.0
                };
                InstantMetricReadTestMain.
                             GIVEN_metric_THEN_read_from_timeSerie(context, TargetApi, TimeSerieUri);
            }

            [Fact]
            public void GIVEN_pitwall_tyres_wear_frontright_percent_THEN_read_from_timeSerie()
            {
                var context = new Context(SimerKey)
                {
                    MetricName = "pitwall_tyres_wear_frontright_percent",
                    PilotName = "IntegrationTest_twear_frontright",
                    SetFieldValue = t => t.TyresWear.FrontRightWear = 61.0,
                    GetApiModelInstance = () => ModelWithTyreWear(),
                    Expected = 61.0
                };
                InstantMetricReadTestMain.
                             GIVEN_metric_THEN_read_from_timeSerie(context, TargetApi, TimeSerieUri);
            }

            [Fact]
            public void GIVEN_pitwall_tyres_wear_frontleft_percent_THEN_read_from_timeSerie()
            {
                var context = new Context(SimerKey)
                {
                    MetricName = "pitwall_tyres_wear_frontleft_percent",
                    PilotName = "IntegrationTest_twear_frontleft",
                    SetFieldValue = t => t.TyresWear.FrontLeftWear = 60.0,
                    GetApiModelInstance = () => ModelWithTyreWear(),
                    Expected = 60.0
                };
                InstantMetricReadTestMain.
                             GIVEN_metric_THEN_read_from_timeSerie(context, TargetApi, TimeSerieUri);
            }
        }

        // =============

        public class TyresTemperaturesTests
        {

            [Fact]
            public void GIVEN_pitwall_tyres_temperatures_rearright_celsius_THEN_read_from_timeSerie()
            {
                var context = new Context(SimerKey)
                {
                    MetricName = "pitwall_tyres_temperatures_rearright_celsius",
                    PilotName = "IntegrationTest_ttemp_rearright",
                    SetFieldValue = t => t.TyresTemperatures.RearRightTemp = 62.0,
                    GetApiModelInstance = () => ModelWithTyreTemp(),
                    Expected = 62.0
                };
                InstantMetricReadTestMain.
                            GIVEN_metric_THEN_read_from_timeSerie(context, TargetApi, TimeSerieUri);
            }

            [Fact]
            public void GIVEN_pitwall_tyres_temperatures_rearleft_celsius_THEN_read_from_timeSerie()
            {
                var context = new Context(SimerKey)
                {
                    MetricName = "pitwall_tyres_temperatures_rearleft_celsius",
                    PilotName = "IntegrationTest_ttemp_rearleft",
                    SetFieldValue = t => t.TyresTemperatures.RearLeftTemp = 62.0,
                    GetApiModelInstance = () => ModelWithTyreTemp(),
                    Expected = 62.0
                };
                InstantMetricReadTestMain.
                            GIVEN_metric_THEN_read_from_timeSerie(context, TargetApi, TimeSerieUri);
            }

            [Fact]
            public void GIVEN_pitwall_tyres_temperatures_frontright_celsius_THEN_read_from_timeSerie()
            {
                var context = new Context(SimerKey)
                {
                    MetricName = "pitwall_tyres_temperatures_frontright_celsius",
                    PilotName = "IntegrationTest_ttemp_frontright",
                    SetFieldValue = t => t.TyresTemperatures.FrontRightTemp = 61.0,
                    GetApiModelInstance = () => ModelWithTyreTemp(),
                    Expected = 61.0
                };
                InstantMetricReadTestMain.
                            GIVEN_metric_THEN_read_from_timeSerie(context, TargetApi, TimeSerieUri);
            }

            [Fact]
            public void GIVEN_pitwall_tyres_temperatures_frontleft_celsius_THEN_read_from_timeSerie()
            {
                var context = new Context(SimerKey)
                {
                    MetricName = "pitwall_tyres_temperatures_frontleft_celsius",
                    PilotName = "IntegrationTest_ttemp_frontleft",
                    SetFieldValue = t => t.TyresTemperatures.FrontLeftTemp = 60.0,
                    GetApiModelInstance = () => ModelWithTyreTemp(),
                    Expected = 60.0
                };
                InstantMetricReadTestMain.
                            GIVEN_metric_THEN_read_from_timeSerie(context, TargetApi, TimeSerieUri);
            }
        }

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

        public static TelemetryModel ModelWithoutSubMappings()
        {
            return new TelemetryModel();
        }

        public static TelemetryModel ModelWithTyreTemp()
        {
            return new TelemetryModel()
            {
                TyresTemperatures = new TyresTemperatures()
            };
        }

        public static TelemetryModel ModelWithTyreWear()
        {
            return new TelemetryModel()
            {
                TyresWear = new TyresWear()
            };
        }
    }
}