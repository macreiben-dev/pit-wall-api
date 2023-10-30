using PitWallDataGatheringApi.Models.Business;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Repositories.Tyres;
using PitWallDataGatheringApi.Repositories.WeatherConditions;

namespace PitWallDataGatheringApi.Services
{
    public sealed class PitwallTelemetryService : IPitwallTelemetryService
    {
        private readonly ITyreWearRepository pitwallTyresPercentRepository;
        private readonly ILaptimeRepository laptimeRepository;
        private readonly ITyresTemperaturesRepository tyresTemperatures;
        private readonly IAvgWetnessRepository avgWetnessRepository;
        private readonly IAirTemperatureRepository airTemperatureRepository;

        public PitwallTelemetryService(
            ITyreWearRepository pitwallTyresPercentRepository,
            ILaptimeRepository laptimeRepository,
            ITyresTemperaturesRepository tyresTemperatures,
            IAvgWetnessRepository avgWetnessRepository,
            IAirTemperatureRepository airTemperatureRepository)
        {
            this.pitwallTyresPercentRepository = pitwallTyresPercentRepository;
            this.laptimeRepository = laptimeRepository;
            this.tyresTemperatures = tyresTemperatures;
            this.avgWetnessRepository = avgWetnessRepository;
            this.airTemperatureRepository = airTemperatureRepository;
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
                tyresTemperatures.UpdateFrontLeft(telemetry.TyresTemperatures, telemetry.PilotName);
                tyresTemperatures.UpdateFrontRight(telemetry.TyresTemperatures, telemetry.PilotName);
                tyresTemperatures.UpdateRearLeft(telemetry.TyresTemperatures, telemetry.PilotName);
                tyresTemperatures.UpdateRearRight(telemetry.TyresTemperatures, telemetry.PilotName);
            }

            // ------

            laptimeRepository.Update(
                telemetry.LaptimeSeconds,
                telemetry.PilotName);

            if (telemetry.AvgWetness != null)
            {
                avgWetnessRepository.Update(
                    telemetry.AvgWetness,
                    telemetry.PilotName);
            }

            if (telemetry.AirTemperature != null)
            {
                airTemperatureRepository.Update(
                    telemetry.AirTemperature,
                    telemetry.PilotName);
            }
        }
    }
}
