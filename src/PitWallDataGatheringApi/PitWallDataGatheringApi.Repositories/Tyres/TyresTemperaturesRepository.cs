using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Prometheus;

namespace PitWallDataGatheringApi.Repositories.Tyres
{
    public sealed class TyresTemperaturesRepository
        : ITyresTemperaturesRepository, IDocumentationTyresTemperaturesSerie
    {
        private const string GaugeName = "pitwall_tyres_temperatures_celsius";

        private const string GaugeNameFrontLeft = "pitwall_tyres_temperatures_frontleft_celsius";
        private const string GaugeNameRearLeft = "pitwall_tyres_temperatures_rearleft_celsius";
        private const string GaugeNameFrontRight = "pitwall_tyres_temperatures_frontright_celsius";
        private const string GaugeNameRearRight = "pitwall_tyres_temperatures_rearright_celsius";

        readonly string[] tyreLabels = new[] { "Pilot" };

        private readonly IGauge _gaugeFrontLeftTyre;
        private readonly IGauge _gaugeRearLeftTyre;
        private readonly IGauge _gaugeFrontRightTyre;
        private readonly IGauge _gaugeRearRightTyre;

        public TyresTemperaturesRepository(IGaugeWrapperFactory gaugeFactory)
        {
            _gaugeFrontLeftTyre = gaugeFactory.Create(GaugeNameFrontLeft, "Front left tyre temperature in celsuis.", ConstantLabels.Labels);
            _gaugeRearLeftTyre = gaugeFactory.Create(GaugeNameRearLeft, "Front left tyre temperature in celsuis.", ConstantLabels.Labels);
            _gaugeFrontRightTyre = gaugeFactory.Create(GaugeNameFrontRight, "Front left tyre temperature in celsuis.", ConstantLabels.Labels);
            _gaugeRearRightTyre = gaugeFactory.Create(GaugeNameRearRight, "Front left tyre temperature in celsuis.", ConstantLabels.Labels);
        }

        public string SerieName => GaugeName;

        public string[] Labels => tyreLabels;

        public string Description => "Current tyre temperature in celsius.";


        public void UpdateFrontLeft(string pilotName, double? frontLeftTemp, CarName carName)
        {
            _gaugeFrontLeftTyre.Update(new[] { pilotName, "All", carName.ToString() }, frontLeftTemp);
        }

        public void UpdateFrontRight(string pilotName, double? frontRightTemp, CarName carName)
        {
            _gaugeFrontRightTyre.Update(new[] { pilotName, "All", carName.ToString() }, frontRightTemp);
        }

        public void UpdateRearLeft(string pilotName, double? rearLeftTemp, CarName carName)
        {
            _gaugeRearLeftTyre.Update(new[] { pilotName, "All", carName.ToString() }, rearLeftTemp);
        }

        public void UpdateRearRight(string pilotName, double? rearRightTemp, CarName carName)
        {
            _gaugeRearRightTyre.Update(new[] { pilotName, "All", carName.ToString() }, rearRightTemp);
        }
    }
}
