using PitWallDataGatheringApi.Repositories.Gauges.Prom;

namespace PitWallDataGatheringApi.Repositories.Leaderboards;

public class MetricLeaderboardDataGauge
{
    public static void Execute(IGauge gauge, MetricData<double?> metric, string sourcePilot, string sourceCar)
    {
        gauge.Update(
            new[] { metric.PilotName.ToString(), metric.CarName.ToString() },
            metric.Data);
    }
}