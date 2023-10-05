using PitWallDataGatheringApi.Models.Apis;
using PitWallDataGatheringApi.Models.Business;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Repositories.Tyres;

namespace PitWallDataGatheringApi.Services
{
    public sealed class PitwallTelemetryService : IPitwallTelemetryService
    {
        private readonly ITyreWearRepository pitwallTyresPercentRepository;
        private readonly ILaptimeRepository laptimeRepository;

        public PitwallTelemetryService(
            ITyreWearRepository pitwallTyresPercentRepository,
            ILaptimeRepository laptimeRepository)
        {
            this.pitwallTyresPercentRepository = pitwallTyresPercentRepository;
            this.laptimeRepository = laptimeRepository;
        }

        public void Update(ITelemetryModel telemetry)
        {
            if (telemetry == null)
            {
                return;
            }

            if (telemetry.TyresWear != null)
            {
                var tyresWears = telemetry.TyresWear;

                pitwallTyresPercentRepository.UpdateFrontLeft(tyresWears);
                pitwallTyresPercentRepository.UpdateFrontRight(tyresWears);
                pitwallTyresPercentRepository.UpdateRearLeft(tyresWears);
                pitwallTyresPercentRepository.UpdateRearRight(tyresWears);
            }

            // ------

            laptimeRepository.Update(telemetry.LaptimeSeconds, telemetry.PilotName);
        }
    }
}
