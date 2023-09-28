using NFluent;
using PitWallDataGatheringApi.Models.Prom;

namespace PitWallDataGatheringApi.Tests.Models.Prom
{
    public class PromLaptimeTest
    {
        [Fact]
        public void GIVEN_pilot_is_Pilot1_and_lapTime_is_120_THEN_pilot1_laptime_is_120()
        {
            PromLapData target = new PromLapData();

            target.WithPilotLaptime("Pilot1", 120);

            Check.That(target.Pilots.Pilot1.LaptimeMilliseconds).IsEqualTo(120);
        }

        [Fact]
        public void GIVEN_pilot_is_Pilot1_and_lapTime_is_120_THEN_pilot2_laptime_is_0()
        {
            PromLapData target = new PromLapData();

            target.WithPilotLaptime("Pilot1", 120);

            Check.That(target.Pilots.Pilot2.LaptimeMilliseconds).IsEqualTo(0);
        }


        [Fact]
        public void GIVEN_pilot_is_Pilot2_and_lapTime_is_120_THEN_pilot1_laptime_is_0()
        {
            PromLapData target = new PromLapData();

            target.WithPilotLaptime("Pilot2", 120);

            Check.That(target.Pilots.Pilot1.LaptimeMilliseconds).IsEqualTo(0);
        }
    }
}
