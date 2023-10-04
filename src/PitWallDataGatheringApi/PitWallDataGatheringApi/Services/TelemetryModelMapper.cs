using AutoMapper;
using PitWallDataGatheringApi.Models.Apis;
using BusinessTelemetryModel = PitWallDataGatheringApi.Models.Business.TelemetryModel;
using BusinessTyresWear = PitWallDataGatheringApi.Models.Business.TyresWear;
using IBusinessTyresWear = PitWallDataGatheringApi.Models.Business.ITyresWear;
using IBusinessTelemetryModel = PitWallDataGatheringApi.Models.Business.ITelemetryModel;


namespace PitWallDataGatheringApi.Services
{
    public sealed class TelemetryModelMapper : ITelemetryModelMapper
    {
        private readonly IMapper _mapper;

        public TelemetryModelMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TelemetryModel, BusinessTelemetryModel>();

                cfg.CreateMap<TyresWear, IBusinessTyresWear>()
                    .ConstructUsing(source =>
                        new BusinessTyresWear()
                        {
                            FrontLeftWear = source.FrontLeftWear,
                            FrontRightWear = source.FrontRightWear,
                            ReartLeftWear = source.ReartLeftWear,
                            RearRightWear = source.RearRightWear,
                        }
                    );
            });

            config.AssertConfigurationIsValid();

            _mapper = config.CreateMapper();
        }

        public IBusinessTelemetryModel Map(TelemetryModel apiModel)
        {
            return _mapper.Map<BusinessTelemetryModel>(apiModel);
        }
    }
}
