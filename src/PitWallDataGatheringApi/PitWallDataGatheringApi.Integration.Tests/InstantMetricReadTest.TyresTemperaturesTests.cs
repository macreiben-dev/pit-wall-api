﻿namespace PitWallDataGatheringApi.Integration.Tests
{
    public partial class InstantMetricReadTest
    {
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
    }
}