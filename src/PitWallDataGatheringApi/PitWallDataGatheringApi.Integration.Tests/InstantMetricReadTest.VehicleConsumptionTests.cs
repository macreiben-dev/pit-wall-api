namespace PitWallDataGatheringApi.Integration.Tests
{
    public partial class InstantMetricReadTest
    {
        public class VehicleConsumptionTests
        {
            private const string CarName = "CarConsumption";

            [Fact]
            public void GIVEN_pitwall_computed_lastlapconsumption_volume_THEN_read_from_timeSerie()
            {
                var context = new Context(SimerKey)
                {
                    MetricName = "pitwall_computed_lastlapconsumption_volume",
                    PilotName = "IntegrationTest_lastlapconsumption",
                    CarName = CarName,
                    SetFieldValue = t => t.VehicleConsumption.ComputedLastLapConsumption = 80.0,
                    GetApiModelInstance = () => ModelWithVehicleConsumption(),
                    Expected = 80.0
                };
                InstantMetricReadTestMain.
                            GIVEN_metric_THEN_read_from_timeSerie(context, TargetApi, TimeSerieUri);
            }

            [Fact]
            public void GIVEN_pitwall_computed_consumedfuelperlap_volume_THEN_read_from_timeSerie()
            {
                var context = new Context(SimerKey)
                {
                    MetricName = "pitwall_computed_consumedfuelperlap_volume",
                    PilotName = "IntegrationTest_consumerfuelperlap",
                    CarName = CarName,
                    SetFieldValue = t => t.VehicleConsumption.ComputedLiterPerLaps = 81.0,
                    GetApiModelInstance = () => ModelWithVehicleConsumption(),
                    Expected = 81.0
                };
                InstantMetricReadTestMain.
                            GIVEN_metric_THEN_read_from_timeSerie(context, TargetApi, TimeSerieUri);
            }

            [Fact]
            public void GIVEN_pitwall_computed_remaininglap_volume_THEN_read_from_timeSerie()
            {
                var context = new Context(SimerKey)
                {
                    MetricName = "pitwall_computed_remaininglap_volume",
                    PilotName = "IntegrationTest_remaininglap_volume",
                    CarName = CarName,
                    SetFieldValue = t => t.VehicleConsumption.ComputedRemainingLaps = 82.0,
                    GetApiModelInstance = () => ModelWithVehicleConsumption(),
                    Expected = 82.0
                };
                InstantMetricReadTestMain.
                            GIVEN_metric_THEN_read_from_timeSerie(context, TargetApi, TimeSerieUri);
            }

            [Fact]
            public void GIVEN_pitwall_fuel_volume_THEN_read_from_timeSerie()
            {
                var context = new Context(SimerKey)
                {
                    MetricName = "pitwall_fuel_volume",
                    PilotName = "IntegrationTest_fuelvol",
                    CarName = CarName,
                    SetFieldValue = t => t.VehicleConsumption.Fuel = 84.0,
                    GetApiModelInstance = () => ModelWithVehicleConsumption(),
                    Expected = 84.0
                };
                InstantMetricReadTestMain.
                            GIVEN_metric_THEN_read_from_timeSerie(context, TargetApi, TimeSerieUri);
            }

            [Fact]
            public void GIVEN_pitwall_maxfuel_volume_THEN_read_from_timeSerie()
            {
                var context = new Context(SimerKey)
                {
                    MetricName = "pitwall_maxfuel_volume",
                    PilotName = "IntegrationTest_maxfuelvol",
                    CarName = CarName,
                    SetFieldValue = t => t.VehicleConsumption.MaxFuel = 85.0,
                    GetApiModelInstance = () => ModelWithVehicleConsumption(),
                    Expected = 85.0
                };
                InstantMetricReadTestMain.
                            GIVEN_metric_THEN_read_from_timeSerie(context, TargetApi, TimeSerieUri);
            }

            [Fact]
            public void GIVEN_pitwall_computed_remainingtimeonfuel_seconds_THEN_read_from_timeSerie()
            {
                var context = new Context(SimerKey)
                {
                    MetricName = "pitwall_computed_remainingtimeonfuel_seconds",
                    PilotName = "IntegrationTest_rtimefuel",
                    CarName = CarName,
                    SetFieldValue = t => t.VehicleConsumption.ComputedRemainingTime = 83.0,
                    GetApiModelInstance = () => ModelWithVehicleConsumption(),
                    Expected = 83.0
                };
                InstantMetricReadTestMain.
                            GIVEN_metric_THEN_read_from_timeSerie(context, TargetApi, TimeSerieUri);
            }
        }
    }
}