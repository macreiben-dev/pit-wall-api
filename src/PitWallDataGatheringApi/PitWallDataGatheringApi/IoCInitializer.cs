using PitWallDataGatheringApi.Repositories.Tyres;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Services;
using PitWallDataGatheringApi.Repositories.WeatherConditions;
using PitWallDataGatheringApi.Repositories.Prometheus;

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
            services.AddSingleton<IGaugeWrapperFactory, GaugeWrapperFactory>();

            services.AddScoped<IDocumentationLaptimeSerie, LaptimeRepository>();
            services.AddScoped<IDocumentationTyresWearSerie, TyreWearRepository>();
            services.AddScoped<IDocumentationTyresTemperaturesSerie, TyresTemperaturesRepository>();
            services.AddScoped<IDocumentationAirTemperatureSerie, AirTemperatureRepository>();
            services.AddScoped<IDocumentationAvgWetnessSerie, AvgWetnessRepository>();

            services.AddSingleton<IPitwallTelemetryService, PitwallTelemetryService>();
            services.AddSingleton<ITelemetryModelMapper, TelemetryModelMapper>();

            services.AddSingleton<ISimerKeyRepository, SimerKeyRepository>();
        }
    }
}
