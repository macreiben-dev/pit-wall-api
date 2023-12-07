using PitWallDataGatheringApi.Repositories.Tyres;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Services;
using PitWallDataGatheringApi.Repositories.WeatherConditions;
using PitWallDataGatheringApi.Repositories.VehicleConsumptions;
using PitWallDataGatheringApi.Repositories.Prom;

namespace PitWallDataGatheringApi
{
    public class IoCInitializer
    {
        public static void Initialize(IServiceCollection services)
        {

            // Add services to the container.

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSingleton<ITyreWearRepository, TyreWearRepository>();
            services.AddSingleton<ILaptimeRepository, LaptimeRepository>();
            services.AddSingleton<ITyresTemperaturesRepository, TyresTemperaturesRepository>();
            services.AddSingleton<IAvgWetnessRepository, AvgWetnessRepository>();
            services.AddSingleton<IAirTemperatureRepository, AirTemperatureRepository>();
            services.AddSingleton<ITrackTemperatureRepository, TrackEmperatureRepository>();
            
            services.AddSingleton<IComputedLastLapConsumptionRepository, ComputedLastLapConsumptionRepository>();
            services.AddSingleton<IComputedLiterPerLapsRepository, ComputedLiterPerLapsRepository>();
            services.AddSingleton<IComputedRemainingLapsRepository, ComputedRemainingLapsRepository>();
            services.AddSingleton<IComputedRemainingTimeRepository, ComputedRemainingTimeRepository>();
            services.AddSingleton<IFuelRepository, FuelRepository>();
            services.AddSingleton<IMaxFuelRepository, MaxFuelRepository>();

            services.AddSingleton<IGaugeFactory, GaugeWrapperFactory>();

            services.AddSingleton<IPitwallTelemetryService, PitwallTelemetryService>();
            services.AddSingleton<ITelemetryModelMapper, TelemetryModelMapper>();

            services.AddSingleton<ISimerKeyRepository, SimerKeyRepository>();
        }
    }
}
