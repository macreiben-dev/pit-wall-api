using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NFluent;
using PitWallDataGatheringApi.Controllers;
using PitWallDataGatheringApi.Controllers.v1;

namespace PitWallDataGatheringApi.Tests
{
    public class IoCInitializerTest
    {
        private IServiceCollection _services;
        private WebApplication _app;

        public IoCInitializerTest()
        {
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddSingleton<SeriesDocumentationController>();
            builder.Services.AddSingleton<TelemetryController>();
            builder.Services.AddSingleton<HealthCheckController>();
            builder.Services.AddSingleton<LeaderboardController>();

            IoCInitializer.Initialize(builder.Services);

            _services = builder.Services;

            _app = builder.Build();
        }

        [Fact]
        public void WHEN_iocPart_defined_THEN_doNot_fail()
        {
            var services = _services
                .Where(c =>
                    c.ServiceType.AssemblyQualifiedName != null  
                    && c.ServiceType.AssemblyQualifiedName.StartsWith("PitWallDataGatheringApi"));

            foreach (var service in services)
            {
                Check.ThatCode(() => 
                    _app.Services.GetService(service.ServiceType))
                    .DoesNotThrow();
            }
        }
    }
}
