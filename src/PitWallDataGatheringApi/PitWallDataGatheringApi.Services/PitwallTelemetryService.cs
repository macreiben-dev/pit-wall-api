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
        private readonly IComputedLastLapConsumptionRepository _laspLapConsumptionRepository;
        private readonly IComputedLiterPerLapsRepository _literPerLapsRepository;
        private readonly IComputedRemainingLapsRepository _remainingLaps;
        private readonly IComputedRemainingTimeRepository _lapConsumptionRepository;
        private readonly IFuelRepository _fuelRepository;
        private readonly IMaxFuelRepository _maxFuelRepository;

        public PitwallTelemetryService(
            ITyreWearRepository pitwallTyresPercentRepository,
            ILaptimeRepository laptimeRepository,
            ITyresTemperaturesRepository tyresTemperatures,
            IAvgWetnessRepository avgWetnessRepository,
            IAirTemperatureRepository airTemperatureRepository,
            ITrackTemperatureRepository trackTemperatureRepository,
            IComputedLastLapConsumptionRepository laspLapConsumptionRepository,
            IComputedLiterPerLapsRepository literPerLapsRepository,
            IComputedRemainingLapsRepository remainingLaps,
            IComputedRemainingTimeRepository lapConsumptionRepository,
            IFuelRepository fuelRepository,
            IMaxFuelRepository maxFuelRepository)
        {
            _pitwallTyresPercentRepository = pitwallTyresPercentRepository;
            _laptimeRepository = laptimeRepository;
            _tyresTemperaturesRepository = tyresTemperatures;
            _avgWetnessRepository = avgWetnessRepository;
            _airTemperatureRepository = airTemperatureRepository;
            _trackTemperatureRepository = trackTemperatureRepository;
            _laspLapConsumptionRepository = laspLapConsumptionRepository;
            _literPerLapsRepository = literPerLapsRepository;
            _remainingLaps = remainingLaps;
            _lapConsumptionRepository = lapConsumptionRepository;
            _fuelRepository = fuelRepository;
            _maxFuelRepository = maxFuelRepository;
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

            UpdateTyreWear(
                telemetry.TyresWear,
                telemetry.PilotName,
                CarName.Null());

            UpdateTyresTemperatures(
                telemetry.TyresTemperatures,
                telemetry.PilotName,
                CarName.Null());

            _laptimeRepository.Update(
                telemetry.LaptimeSeconds,
                telemetry.PilotName, 
                CarName.Null());

            telemetry.AvgWetness.WhenHasValue(() => 
                _avgWetnessRepository.Update(
                    telemetry.AvgWetness,
                    telemetry.PilotName,
                    CarName.Null()
                    )
                );

            telemetry.AirTemperature.WhenHasValue(() =>
                _airTemperatureRepository.Update(
                    telemetry.AirTemperature,
                    telemetry.PilotName,
                    CarName.Null()
                    )
                );

            telemetry.TrackTemperature.WhenHasValue(() =>
                _trackTemperatureRepository.Update(
                    telemetry.TrackTemperature,
                    telemetry.PilotName,
                    CarName.Null()
                    )
                );
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
                    carName ));

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
