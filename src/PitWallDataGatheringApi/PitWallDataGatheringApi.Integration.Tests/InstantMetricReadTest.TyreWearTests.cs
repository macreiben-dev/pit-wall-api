namespace PitWallDataGatheringApi.Integration.Tests
{
    public partial class InstantMetricReadTest
    {
        public class TyreWearTests
        {
            private const string CarName = "CarWear";

            [Fact]
            public void GIVEN_pitwall_tyres_wear_rearleft_percent_THEN_read_from_timeSerie()
            {
                var context = new Context(SimerKey)
                {
                    MetricName = "pitwall_tyres_wear_rearleft_percent",
                    PilotName = "IntegrationTest_twear_rearleft",
                    CarName = CarName,
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
                    CarName = CarName,
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
                    CarName = CarName,
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
                    CarName = CarName,
                    SetFieldValue = t => t.TyresWear.FrontLeftWear = 60.0,
                    GetApiModelInstance = () => ModelWithTyreWear(),
                    Expected = 60.0
                };
                InstantMetricReadTestMain.
                             GIVEN_metric_THEN_read_from_timeSerie(context, TargetApi, TimeSerieUri);
            }
        }
    }
}