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
        private readonly ITyresTemperaturesRepository tyresTemperaturesRepository;
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
            this.tyresTemperaturesRepository = tyresTemperatures;
            this.avgWetnessRepository = avgWetnessRepository;
            this.airTemperatureRepository = airTemperatureRepository;
        }

        public void Update(ITelemetryModel telemetry)
        {
            if (telemetry == null)
            {
                return;
            }

            /**
             * Possible design issue here, tyres wear can be null, and tyre temp cannot.
             * 
             * Maybe uniformize all this.
             * */

            telemetry.TyresWear.WhenHasValue(() => UpdateTyreWear(telemetry.TyresWear, telemetry.PilotName));

            telemetry.TyresTemperatures.WhenHasValue(() => UpdateTyresTemperatures(telemetry.TyresTemperatures, telemetry.PilotName));

            laptimeRepository.Update(
                telemetry.LaptimeSeconds,
                telemetry.PilotName);

            telemetry.AvgWetness.WhenHasValue(
                () => avgWetnessRepository.Update(
                    telemetry.AvgWetness,
                    telemetry.PilotName)
                );

            telemetry.AirTemperature.WhenHasValue(
                () =>
                airTemperatureRepository.Update(
                    telemetry.AirTemperature,
                    telemetry.PilotName)
                );
        }

        private void UpdateTyresTemperatures(ITyresTemperatures tyresTemperatures, string pilotName)
        {
            tyresTemperaturesRepository.UpdateFrontLeft(tyresTemperatures, pilotName);
            tyresTemperaturesRepository.UpdateFrontRight(tyresTemperatures, pilotName);
            tyresTemperaturesRepository.UpdateRearLeft(tyresTemperatures, pilotName);
            tyresTemperaturesRepository.UpdateRearRight(tyresTemperatures, pilotName);
        }

        private void UpdateTyreWear(ITyresWear? tyresWears, string pilotName)
        {
            pitwallTyresPercentRepository.UpdateFrontLeft(tyresWears, pilotName);
            pitwallTyresPercentRepository.UpdateFrontRight(tyresWears, pilotName);
            pitwallTyresPercentRepository.UpdateRearLeft(tyresWears, pilotName);
            pitwallTyresPercentRepository.UpdateRearRight(tyresWears, pilotName);
        }
    }
}
