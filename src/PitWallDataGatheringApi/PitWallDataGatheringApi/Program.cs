using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Repositories.Tyres;
using PitWallDataGatheringApi.Services;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITyreWearRepository, TyreWearRepository>();
builder.Services.AddSingleton<ILaptimeRepository, LaptimeRepository>();

builder.Services.AddScoped<IDocumentationLaptimeSerie, LaptimeRepository>();
builder.Services.AddScoped<IDocumentationTyresWearSerie, TyreWearRepository>();
builder.Services.AddScoped<IDocumentationTyresTemperaturesSerie, TyresTemperaturesRepository>();

builder.Services.AddScoped<IPitwallTelemetryService, PitwallTelemetryService>();
builder.Services.AddScoped<ITelemetryModelMapper, TelemetryModelMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapMetrics();
});

app.Run();
