using PitWallDataGatheringApi.Models.Apis.v1;

namespace PitWallDataGatheringApi.Integration.Tests
{
    public partial class InstantMetricReadTest
    {
        private const string TimeSerieUri = "http://localhost:10100";
        private const string TargetApi = "http://localhost:32773";
        private const string SimerKey = "some_test_looking_value23";

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

        public static TelemetryModel ModelWithVehicleConsumption()
        {
            return new TelemetryModel()
            {
                VehicleConsumption = new VehicleConsumption()
            };
        }
    }
}