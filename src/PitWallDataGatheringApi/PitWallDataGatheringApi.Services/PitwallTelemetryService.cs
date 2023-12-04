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
        private readonly ITyreWearRepository _pitwallTyresPercentRepository;
        private readonly ILaptimeRepository _laptimeRepository;
        private readonly ITyresTemperaturesRepository _tyresTemperaturesRepository;
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
            ITyreWearRepository pitwallTyresPercentRepository,
            ILaptimeRepository laptimeRepository,
            ITyresTemperaturesRepository tyresTemperatures,
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

            _laptimeRepository.Update(new MetricData<double?>(
                telemetry.LaptimeSeconds,
                telemetry.PilotName,
                telemetry.CarName));

            telemetry.AvgWetness.WhenHasValue(() =>
                _avgWetnessRepository.Update(new MetricData<double?>(
                    telemetry.AvgWetness,
                    telemetry.PilotName,
                    telemetry.CarName
                    ))
                );

            telemetry.AirTemperature.WhenHasValue(() =>
                _airTemperatureRepository.Update(new MetricData<double?>(
                    telemetry.AirTemperature,
                    telemetry.PilotName,
                    telemetry.CarName
                    ))
                );

            telemetry.TrackTemperature.WhenHasValue(() =>
                _trackTemperatureRepository.Update(new MetricData<double?>(
                    telemetry.TrackTemperature,
                    telemetry.PilotName,
                    telemetry.CarName
                    ))
                );
        }

        private void UpdateVehicleConsumption(
            IVehicleConsumption vehicleConsumption,
            PilotName pilotName,
            CarName carName)
        {
            vehicleConsumption.ComputedLastLapConsumption.WhenHasValue(() =>
                _lastLapConsumptionRepository.Update(new MetricData<double?>(
                    vehicleConsumption.ComputedLastLapConsumption,
                    pilotName,
                    carName)));

            vehicleConsumption.ComputedLiterPerLaps.WhenHasValue(() =>
                _literPerLapsRepository.Update(new MetricData<double?>(
                    vehicleConsumption.ComputedLiterPerLaps,
                    pilotName,
                    carName)));

            vehicleConsumption.ComputedRemainingLaps.WhenHasValue(() =>
                _remainingLapsRepository.Update(new MetricData<double?>(
                    vehicleConsumption.ComputedRemainingLaps,
                    pilotName,
                    carName)));

            vehicleConsumption.ComputedRemainingTime.WhenHasValue(() =>
                _remainingTimeRepository.Update(new MetricData<double?>(
                    vehicleConsumption.ComputedRemainingTime,
                    pilotName,
                    carName)));

            vehicleConsumption.Fuel.WhenHasValue(() =>
                _fuelRepository.Update(new MetricData<double?>(
                    vehicleConsumption.Fuel,
                    pilotName,
                    carName
                    )));

            vehicleConsumption.MaxFuel.WhenHasValue(() =>
                _maxFuelRepository.Update(new MetricData<double?>(
                    vehicleConsumption.MaxFuel,
                    pilotName,
                    carName
                    )));
        }

        private void UpdateTyresTemperatures(ITyresTemperatures tyresTemperatures, PilotName pilotName, CarName carName)
        {
            tyresTemperatures.FrontLeftTemp.WhenHasValue(
                () => _tyresTemperaturesRepository.UpdateFrontLeft(new MetricData<double?>(
                    tyresTemperatures.FrontLeftTemp,
                    pilotName,
                    carName)));

            tyresTemperatures.FrontRightTemp.WhenHasValue(
                () => _tyresTemperaturesRepository.UpdateFrontRight(new MetricData<double?>(
                    tyresTemperatures.FrontRightTemp,
                    pilotName,
                    carName)));

            tyresTemperatures.RearLeftTemp.WhenHasValue(
                () => _tyresTemperaturesRepository.UpdateRearLeft(new MetricData<double?>(
                    tyresTemperatures.RearLeftTemp,
                    pilotName,
                    carName)));

            tyresTemperatures.RearRightTemp.WhenHasValue(
                () => _tyresTemperaturesRepository.UpdateRearRight(new MetricData<double?>(
                    tyresTemperatures.RearRightTemp,
                    pilotName,
                    carName)));
        }

        private void UpdateTyreWear(ITyresWear tyresWears, PilotName pilotName, CarName carName)
        {
            tyresWears.FrontLeftWear.WhenHasValue(() =>
                _pitwallTyresPercentRepository.UpdateFrontLeft(new MetricData<double?>(
                    tyresWears.FrontLeftWear,
                    pilotName,
                    carName)));

            tyresWears.FrontRightWear.WhenHasValue(() =>
                _pitwallTyresPercentRepository.UpdateFrontRight(new MetricData<double?>(
                    tyresWears.FrontRightWear,
                    pilotName,
                    carName)));

            tyresWears.ReartLeftWear.WhenHasValue(() =>
                _pitwallTyresPercentRepository.UpdateRearLeft(new MetricData<double?>(
                    tyresWears.ReartLeftWear,
                    pilotName,
                    carName)));

            tyresWears.RearRightWear.WhenHasValue(() =>
                _pitwallTyresPercentRepository.UpdateRearRight(new MetricData<double?>(
                    tyresWears.RearRightWear,
                    pilotName,
                    carName)));
        }
    }
}
