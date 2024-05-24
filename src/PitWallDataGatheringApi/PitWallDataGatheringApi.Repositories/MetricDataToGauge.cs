using PitWallDataGatheringApi.Repositories.Gauges.Prom;

namespace PitWallDataGatheringApi.Repositories
{
    public static class MetricDataToGauge
    {
        public static void Execute(IGauge gauge, MetricData<double?> metric)
        {
            gauge.Update(
                new[] { metric.PilotName.ToString(), metric.CarName.ToString() },
                metric.Data);
        }
        
        public static void Execute(IGauge gauge, MetricDataWithSource<double?> metric)
        {
            gauge.Update(
                new[]
                {
                    metric.PilotName.ToString(),
                    metric.CarName.ToString(),
                    metric.SourcePilotName.ToString(),
                    metric.SourceCarName.ToString()
                },
                metric.Data);
        }
    }
}
