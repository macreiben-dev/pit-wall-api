using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Prometheus;

namespace PitWallDataGatheringApi.Repositories.Tyres
{
    public sealed class TyreWearRepository : ITyreWearRepository, IDocumentationTyresWearSerie
    {
        private const string GaugeNamePitwallTyreWearPercent = "pitwall_tyreswear_percent";

        private const string GaugeNameFrontLeft = "pitwall_tyres_wear_frontleft_percent";
        private const string GaugeNameRearLeft = "pitwall_tyres_wear_rearleft_percent";
        private const string GaugeNameFrontRight = "pitwall_tyres_wear_frontright_percent";
        private const string GaugeNameRearRight = "pitwall_tyres_wear_rearright_percent";

        readonly string[] tyreLabels = new[] { "Pilot" };

        private readonly IGauge _gaugeFrontLeft;
        private readonly IGauge _gaugeRearLeft;
        private readonly IGauge _gaugeFrontRight;
        private readonly IGauge _gaugeRearRight;

        public TyreWearRepository(IGaugeWrapperFactory gaugeFactory)
        {
            _gaugeFrontLeft = gaugeFactory.Create(GaugeNameFrontLeft, "Tyres wear front left in percent.", ConstantLabels.Labels);
            _gaugeRearLeft = gaugeFactory.Create(GaugeNameRearLeft, "Tyres wear front left in percent.", ConstantLabels.Labels);
            _gaugeFrontRight = gaugeFactory.Create(GaugeNameFrontRight, "Tyres wear front left in percent.", ConstantLabels.Labels);
            _gaugeRearRight = gaugeFactory.Create(GaugeNameRearRight, "Tyres wear front left in percent.", ConstantLabels.Labels);
        }

        public string SerieName => GaugeNamePitwallTyreWearPercent;

        public string[] Labels => tyreLabels;

        public string Description => "Current tyre wear as percent.";

        public void UpdateFrontLeft(string pilotName, double? frontLeftWear, CarName carName)
        {
            _gaugeFrontLeft.Update(new[] { pilotName, "All", carName.ToString() }, frontLeftWear);
        }

        public void UpdateFrontRight(string pilotName, double? frontRightWear, CarName carName)
        {
            _gaugeFrontRight.Update(new[] { pilotName, "All", carName.ToString() }, frontRightWear);
        }

        public void UpdateRearLeft(string pilotName, double? reartLeftWear, CarName carName)
        {
            _gaugeRearLeft.Update(new[] { pilotName, "All", carName.ToString() }, reartLeftWear);
        }

        public void UpdateRearRight(string pilotName, double? rearRightWear, CarName carName)
        {
            _gaugeRearRight.Update(new[] { pilotName, "All", carName.ToString() }, rearRightWear);
        }
    }
}
