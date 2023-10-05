using PitWallDataGatheringApi.Models.Business;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Repositories.Tyres;

namespace PitWallDataGatheringApi.Services
{
    public sealed class PitwallTelemetryService : IPitwallTelemetryService
    {
        private readonly ITyreWearRepository pitwallTyresPercentRepository;
        private readonly ILaptimeRepository laptimeRepository;
        private readonly ITyresTemperaturesRepository tyresTemperatures;

        public PitwallTelemetryService(
            ITyreWearRepository pitwallTyresPercentRepository,
            ILaptimeRepository laptimeRepository,
            ITyresTemperaturesRepository tyresTemperatures)
        {
            this.pitwallTyresPercentRepository = pitwallTyresPercentRepository;
            this.laptimeRepository = laptimeRepository;
            this.tyresTemperatures = tyresTemperatures;
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

            if (telemetry.TyresTemperatures != null)
            {
                tyresTemperatures.UpdateFrontLeft(telemetry.TyresTemperatures);
                tyresTemperatures.UpdateFrontRight(telemetry.TyresTemperatures);
                tyresTemperatures.UpdateRearLeft(telemetry.TyresTemperatures);
                tyresTemperatures.UpdateRearRight(telemetry.TyresTemperatures);
            }

            // ------

            laptimeRepository.Update(telemetry.LaptimeSeconds, telemetry.PilotName);
        }
    }
}
