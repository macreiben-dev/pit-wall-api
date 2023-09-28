﻿using NFluent;
using PitWallDataGatheringApi.Models.Prom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
