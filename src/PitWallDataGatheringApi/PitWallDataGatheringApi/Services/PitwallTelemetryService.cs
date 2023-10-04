using PitWallDataGatheringApi.Models.Apis;
using PitWallDataGatheringApi.Repositories;
using Prometheus;

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

        public void Update(TelemetryModel telemetry)
        {
            if (telemetry == null)
            {
                return;
            }

            if (telemetry.Tyres != null)
            {
                var tyresWears = telemetry.Tyres;

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
