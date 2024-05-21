using PitWallDataGatheringApi;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Repositories.Leaderboards.Initializations;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

IoCInitializer.Initialize(builder.Services);

var app = builder.Build();

var simerKey = app.Services.GetService<ISimerKeyRepository>()
    .Key;

app.Services.GetService<ILeaderboardDatabaseInitializer>().Init();


if (string.IsNullOrEmpty(simerKey))
{
    app.Logger.Log(LogLevel.Error, $"Simer key loaded: [{simerKey}]");
}
else
{
    app.Logger.Log(LogLevel.Information, $"Simer key loaded: [{simerKey}]");
}

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
