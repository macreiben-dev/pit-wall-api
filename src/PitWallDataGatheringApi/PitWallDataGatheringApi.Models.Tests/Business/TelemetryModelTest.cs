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

        [Fact]
        public void THEN_cannot_set_tyresWear_toNull()
        {
            TelemetryModel target = new TelemetryModel();

            Check.ThatCode(() => target.TyresWear = null)
                .Throws<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'Tyre wears')");
        }

        [Fact]
        public void THEN_cannot_set_tyresTemperatures_toNull()
        {
            TelemetryModel target = new TelemetryModel();

            Check.ThatCode(() => target.TyresTemperatures = null)
                .Throws<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'Tyres temperatures')");
        }
    }
}
