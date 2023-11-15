using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Models.Business;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Repositories.Tyres;
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

        public PitwallTelemetryService(
            ITyreWearRepository pitwallTyresPercentRepository,
            ILaptimeRepository laptimeRepository,
            ITyresTemperaturesRepository tyresTemperatures,
            IAvgWetnessRepository avgWetnessRepository,
            IAirTemperatureRepository airTemperatureRepository, 
            ITrackTemperatureRepository trackTemperatureRepository)
        {
            _pitwallTyresPercentRepository = pitwallTyresPercentRepository;
            _laptimeRepository = laptimeRepository;
            _tyresTemperaturesRepository = tyresTemperatures;
            _avgWetnessRepository = avgWetnessRepository;
            _airTemperatureRepository = airTemperatureRepository;
            _trackTemperatureRepository = trackTemperatureRepository;
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
                telemetry.PilotName);

            UpdateTyresTemperatures(
                telemetry.TyresTemperatures,
                telemetry.PilotName);

            _laptimeRepository.Update(
                telemetry.LaptimeSeconds,
                telemetry.PilotName);

            telemetry.AvgWetness.WhenHasValue(() => 
                _avgWetnessRepository.Update(
                    telemetry.AvgWetness,
                    telemetry.PilotName)
                );

            telemetry.AirTemperature.WhenHasValue(() =>
                _airTemperatureRepository.Update(
                    telemetry.AirTemperature,
                    telemetry.PilotName,
                    CarName.Null())
                );

            telemetry.TrackTemperature.WhenHasValue(() =>
                _trackTemperatureRepository.Update(
                    telemetry.TrackTemperature,
                    telemetry.PilotName,
                    CarName.Null()
                    )
                );
        }

        private void UpdateTyresTemperatures(ITyresTemperatures tyresTemperatures, string pilotName)
        {
            tyresTemperatures.FrontLeftTemp.WhenHasValue(
                () => _tyresTemperaturesRepository.UpdateFrontLeft(
                    pilotName, 
                    tyresTemperatures.FrontLeftTemp, 
                    CarName.Null()));

            tyresTemperatures.FrontRightTemp.WhenHasValue(
                () => _tyresTemperaturesRepository.UpdateFrontRight(
                    pilotName,
                    tyresTemperatures.FrontRightTemp,
                    CarName.Null()));

            tyresTemperatures.RearLeftTemp.WhenHasValue(
                () => _tyresTemperaturesRepository.UpdateRearLeft(
                    pilotName,
                    tyresTemperatures.RearLeftTemp,
                    CarName.Null()));

            tyresTemperatures.RearRightTemp.WhenHasValue(
                () => _tyresTemperaturesRepository.UpdateRearRight(
                    pilotName,
                    tyresTemperatures.RearRightTemp,
                    CarName.Null()));
        }

        private void UpdateTyreWear(ITyresWear tyresWears, string pilotName)
        {
            tyresWears.FrontLeftWear.WhenHasValue(() =>
                _pitwallTyresPercentRepository.UpdateFrontLeft(
                    pilotName, 
                    tyresWears.FrontLeftWear, 
                    CarName.Null()));

            tyresWears.FrontRightWear.WhenHasValue(() =>
                _pitwallTyresPercentRepository.UpdateFrontRight(
                    pilotName, 
                    tyresWears.FrontRightWear, 
                    CarName.Null()));

            tyresWears.ReartLeftWear.WhenHasValue(() =>
                _pitwallTyresPercentRepository.UpdateRearLeft(
                    pilotName, 
                    tyresWears.ReartLeftWear, 
                    CarName.Null()));

            tyresWears.RearRightWear.WhenHasValue(() =>
                _pitwallTyresPercentRepository.UpdateRearRight(
                    pilotName, 
                    tyresWears.RearRightWear, 
                    CarName.Null()));
        }
    }
}
