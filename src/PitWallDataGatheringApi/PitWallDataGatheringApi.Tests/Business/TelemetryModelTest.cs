using NFluent;
using PitWallDataGatheringApi.Models.Business;

namespace PitWallDataGatheringApi.Tests.Business
{
    public class TelemetryModelTest
    {
        [Fact]
        public void THEN_tyreTemperature_isNotNull()
        {
            TelemetryModel target = new TelemetryModel();

            Check.That(target.TyresTemperatures).IsNotNull();
        }

        [Fact]
        public void THEN_tyreWear_isNotNull()
        {
            TelemetryModel target = new TelemetryModel();

            Check.That(target.TyresWear).IsNotNull();
        }
    }
}
