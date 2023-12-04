using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Prom;
using PitWallDataGatheringApi.Repositories.VehicleConsumptions;

namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public sealed class AvgWetnessRepository : IAvgWetnessRepository
    {
        private const string LocalSerieName = "pitwall_road_wetness_avg_percent";
        private readonly IGauge _gauge;

        public AvgWetnessRepository(IGaugeWrapperFactory gaugeFactory)
        {
            _gauge = gaugeFactory.Create(
               LocalSerieName,
              "Average road wetness in percent.",
              ConstantLabels.Labels);
        }

        public void Update(double? data, string pilotName, CarName carName)
        {
            Update(new MetricData<double?>(data, new PilotName(pilotName), carName));
        }

        public void Update(MetricData<double?> metric)
        {
            MetricDataToGauge.Execute(_gauge, metric);
        }
    }
}
