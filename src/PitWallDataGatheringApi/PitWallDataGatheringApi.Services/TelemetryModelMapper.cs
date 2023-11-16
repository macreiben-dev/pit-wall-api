using PitWallDataGatheringApi.Models.Apis;

using BusinessTelemetryModel = PitWallDataGatheringApi.Models.Business.TelemetryModel;
using BusinessTyresWear = PitWallDataGatheringApi.Models.Business.TyresWear;
using BusinessTyresTemperatures = PitWallDataGatheringApi.Models.Business.TyresTemperatures;
using BusinessVehicleConsumption = PitWallDataGatheringApi.Models.Business.VehicleConsumption;
using IBusinessTyresWear = PitWallDataGatheringApi.Models.Business.ITyresWear;
using IBusinessTelemetryModel = PitWallDataGatheringApi.Models.Business.ITelemetryModel;
using IBusinessTyresTemperatures = PitWallDataGatheringApi.Models.Business.ITyresTemperatures;
using IBusinessVehicleConsumption = PitWallDataGatheringApi.Models.Business.IVehicleConsumption;

namespace PitWallDataGatheringApi.Services
{
    public sealed class TelemetryModelMapper : ITelemetryModelMapper
    {
        public IBusinessTelemetryModel Map(TelemetryModel apiModel)
        {
            BusinessTelemetryModel model = new BusinessTelemetryModel();

            model.AirTemperature = apiModel.AirTemperature;
            
            model.AvgWetness = apiModel.AvgWetness;

            model.TrackTemperature = apiModel.TrackTemperature;
            
            model.LaptimeSeconds = apiModel.LaptimeSeconds;
            
            /**
             * correct this one in v2 API
             * */
            model.PilotName = apiModel.PilotName;

            model.TyresWear = Map(apiModel.TyresWear);
            
            model.TyresTemperatures = Map(apiModel.TyresTemperatures);

            model.VehicleConsumption = Map(apiModel.VehicleConsumption);

            return model;
        }

        private IBusinessVehicleConsumption Map(VehicleConsumption? vehicleConsumption)
        {
            if(vehicleConsumption != null
                && (vehicleConsumption.Fuel != null
                || vehicleConsumption.MaxFuel != null
                || vehicleConsumption.ComputedLiterPerLaps != null
                || vehicleConsumption.ComputedRemainingLaps != null
                || vehicleConsumption.ComputedRemainingTime != null
                )
               )
            {
                return new BusinessVehicleConsumption()
                {
                    Fuel = vehicleConsumption.Fuel,
                    MaxFuel = vehicleConsumption.MaxFuel,
                    ComputedLiterPerLaps = vehicleConsumption.ComputedLiterPerLaps,
                    ComputedRemainingLaps = vehicleConsumption.ComputedRemainingLaps,
                    ComputedRemainingTime = vehicleConsumption.ComputedRemainingTime,
                };
            }

            return new BusinessVehicleConsumption();
        }

        private IBusinessTyresTemperatures Map(TyresTemperatures? source)
        {
            if (AnyTemperatureIsProvided(source))
            {
                return new BusinessTyresTemperatures()
                {
                    FrontLeftTemp = source.FrontLeftTemp,
                    FrontRightTemp = source.FrontRightTemp,
                    RearLeftTemp = source.RearLeftTemp,
                    RearRightTemp = source.RearRightTemp,
                };
            }

            return new BusinessTyresTemperatures();
        }

        private IBusinessTyresWear Map(TyresWear? source)
        {
            if (AnyTyreWearIsProvided(source))
            {
                return new BusinessTyresWear()
                {
                    FrontLeftWear = source.FrontLeftWear,
                    FrontRightWear = source.FrontRightWear,
                    ReartLeftWear = source.ReartLeftWear,
                    RearRightWear = source.RearRightWear,
                };
            }

            return new BusinessTyresWear();
        }

        private static bool AnyTyreWearIsProvided(TyresWear source)
        {
            return source != null && (
                source.FrontLeftWear.HasValue
                || source.FrontRightWear.HasValue
                || source.ReartLeftWear.HasValue
                || source.RearRightWear.HasValue);
        }

        private static bool AnyTemperatureIsProvided(TyresTemperatures source)
        {
            return source != null && (
                source.FrontLeftTemp.HasValue
                || source.FrontRightTemp.HasValue
                || source.RearLeftTemp.HasValue
                || source.RearRightTemp.HasValue);
        }

    }
}
