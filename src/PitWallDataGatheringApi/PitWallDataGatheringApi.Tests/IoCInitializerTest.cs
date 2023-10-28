using Microsoft.AspNetCore.Builder;

namespace PitWallDataGatheringApi.Tests
{
    public class IoCInitializerTest
    {
        [Fact]
        public void WHEN_iocPart_defined_THEN_doNot_fail()
        {
            var builder = WebApplication.CreateBuilder();

            IoCInitializer.Initialize(builder.Services);

            var app = builder.Build();

            var services = builder.Services
                .Where(c => c.ServiceType.AssemblyQualifiedName.StartsWith("PitWallDataGatheringApi"));

            foreach (var service in services)
            {
                var current = app.Services.GetService(service.ServiceType);
            }
        }
    }
}
