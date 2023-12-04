using Microsoft.Extensions.Logging;
using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Models.Business;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Repositories.Tyres;
using PitWallDataGatheringApi.Repositories.VehicleConsumptions;
using PitWallDataGatheringApi.Repositories.WeatherConditions;

namespace PitWallDataGatheringApi.Services
{
    public sealed class PitwallTelemetryService : IPitwallTelemetryService
    {
        private readonly ITyreWearRepositoryLegacy _pitwallTyresPercentRepository;
        private readonly ILaptimeRepository _laptimeRepository;
        private readonly ITyresTemperaturesRepositoryLegacy _tyresTemperaturesRepository;
        private readonly IAvgWetnessRepository _avgWetnessRepository;
        private readonly IAirTemperatureRepository _airTemperatureRepository;
        private readonly ITrackTemperatureRepository _trackTemperatureRepository;
        private readonly IComputedLastLapConsumptionRepository _lastLapConsumptionRepository;
        private readonly IComputedLiterPerLapsRepository _literPerLapsRepository;
        private readonly IComputedRemainingLapsRepository _remainingLapsRepository;
        private readonly IComputedRemainingTimeRepository _remainingTimeRepository;
        private readonly IFuelRepository _fuelRepository;
        private readonly IMaxFuelRepository _maxFuelRepository;
        private readonly ILogger<PitwallTelemetryService> _logger;

        public PitwallTelemetryService(
            ITyreWearRepositoryLegacy pitwallTyresPercentRepository,
            ILaptimeRepository laptimeRepository,
            ITyresTemperaturesRepositoryLegacy tyresTemperatures,
            IAvgWetnessRepository avgWetnessRepository,
            IAirTemperatureRepository airTemperatureRepository,
            ITrackTemperatureRepository trackTemperatureRepository,
            IComputedLastLapConsumptionRepository lastLapConsumptionRepository,
            IComputedLiterPerLapsRepository literPerLapsRepository,
            IComputedRemainingLapsRepository remainingLaps,
            IComputedRemainingTimeRepository remainingTimeRepository,
            IFuelRepository fuelRepository,
            IMaxFuelRepository maxFuelRepository,
            ILogger<PitwallTelemetryService> logger)
        {
            _pitwallTyresPercentRepository = pitwallTyresPercentRepository;
            _laptimeRepository = laptimeRepository;
            _tyresTemperaturesRepository = tyresTemperatures;
            _avgWetnessRepository = avgWetnessRepository;
            _airTemperatureRepository = airTemperatureRepository;
            _trackTemperatureRepository = trackTemperatureRepository;

            _lastLapConsumptionRepository = lastLapConsumptionRepository;
            _literPerLapsRepository = literPerLapsRepository;
            _remainingLapsRepository = remainingLaps;
            _remainingTimeRepository = remainingTimeRepository;
            _fuelRepository = fuelRepository;
            _maxFuelRepository = maxFuelRepository;

            _logger = logger;

            _logger.LogInformation("Pitwall telemetry service ready.");
        }

        public void Update(ITelemetryModel telemetry)
        {
            if (telemetry == null)
            {
                return;
            }

            /**
             * Idea : pilotname is a perrequest information, and could be stored in an injected repository.
             * */

            UpdateVehicleConsumption(
                telemetry.VehicleConsumption,
                telemetry.PilotName,
                telemetry.CarName);

            UpdateTyreWear(
                telemetry.TyresWear,
                telemetry.PilotName,
                telemetry.CarName);

            UpdateTyresTemperatures(
                telemetry.TyresTemperatures,
                telemetry.PilotName,
                telemetry.CarName);

            _laptimeRepository.Update(
                telemetry.LaptimeSeconds,
                telemetry.PilotName,
                telemetry.CarName);

            telemetry.AvgWetness.WhenHasValue(() =>
                _avgWetnessRepository.Update(
                    telemetry.AvgWetness,
                    telemetry.PilotName,
                    telemetry.CarName
                    )
                );

            telemetry.AirTemperature.WhenHasValue(() =>
                _airTemperatureRepository.Update(
                    telemetry.AirTemperature,
                    telemetry.PilotName,
                    telemetry.CarName
                    )
                );

            telemetry.TrackTemperature.WhenHasValue(() =>
                _trackTemperatureRepository.Update(
                    telemetry.TrackTemperature,
                    telemetry.PilotName,
                    telemetry.CarName
                    )
                );
        }

        private void UpdateVehicleConsumption(
            IVehicleConsumption vehicleConsumption,
            string pilotName,
            CarName carName)
        {
            vehicleConsumption.ComputedLastLapConsumption.WhenHasValue(() =>
                _lastLapConsumptionRepository.Update(
                    vehicleConsumption.ComputedLastLapConsumption,
                    pilotName,
                    carName));

            vehicleConsumption.ComputedLiterPerLaps.WhenHasValue(() =>
                _literPerLapsRepository.Update(
                    vehicleConsumption.ComputedLiterPerLaps,
                    pilotName,
                    carName));

            vehicleConsumption.ComputedRemainingLaps.WhenHasValue(() =>
                _remainingLapsRepository.Update(
                    vehicleConsumption.ComputedRemainingLaps,
                    pilotName,
                    carName));

            vehicleConsumption.ComputedRemainingTime.WhenHasValue(() =>
                _remainingTimeRepository.Update(
                    vehicleConsumption.ComputedRemainingTime,
                    pilotName,
                    carName));

            vehicleConsumption.Fuel.WhenHasValue(() =>
                _fuelRepository.Update(
                    vehicleConsumption.Fuel,
                    pilotName,
                    carName
                    ));

            vehicleConsumption.MaxFuel.WhenHasValue(() =>
                _maxFuelRepository.Update(
                    vehicleConsumption.MaxFuel,
                    pilotName,
                    carName
                    ));
        }

        private void UpdateTyresTemperatures(ITyresTemperatures tyresTemperatures, string pilotName, CarName carName)
        {
            tyresTemperatures.FrontLeftTemp.WhenHasValue(
                () => _tyresTemperaturesRepository.UpdateFrontLeft(
                    pilotName,
                    tyresTemperatures.FrontLeftTemp,
                    carName));

            tyresTemperatures.FrontRightTemp.WhenHasValue(
                () => _tyresTemperaturesRepository.UpdateFrontRight(
                    pilotName,
                    tyresTemperatures.FrontRightTemp,
                    carName));

            tyresTemperatures.RearLeftTemp.WhenHasValue(
                () => _tyresTemperaturesRepository.UpdateRearLeft(
                    pilotName,
                    tyresTemperatures.RearLeftTemp,
                    carName));

            tyresTemperatures.RearRightTemp.WhenHasValue(
                () => _tyresTemperaturesRepository.UpdateRearRight(
                    pilotName,
                    tyresTemperatures.RearRightTemp,
                    carName));
        }

        private void UpdateTyreWear(ITyresWear tyresWears, string pilotName, CarName carName)
        {
            tyresWears.FrontLeftWear.WhenHasValue(() =>
                _pitwallTyresPercentRepository.UpdateFrontLeft(
                    pilotName,
                    tyresWears.FrontLeftWear,
                    carName));

            tyresWears.FrontRightWear.WhenHasValue(() =>
                _pitwallTyresPercentRepository.UpdateFrontRight(
                    pilotName,
                    tyresWears.FrontRightWear,
                    carName));

            tyresWears.ReartLeftWear.WhenHasValue(() =>
                _pitwallTyresPercentRepository.UpdateRearLeft(
                    pilotName,
                    tyresWears.ReartLeftWear,
                    carName));

            tyresWears.RearRightWear.WhenHasValue(() =>
                _pitwallTyresPercentRepository.UpdateRearRight(
                    pilotName,
                    tyresWears.RearRightWear,
                    carName));
        }
    }
}
