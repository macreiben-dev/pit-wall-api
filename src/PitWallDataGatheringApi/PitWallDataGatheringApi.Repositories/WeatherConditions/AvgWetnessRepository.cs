using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Prom;

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

        public void Update(double? laptime, string pilotName, CarName carName)
        {
            _gauge.Update(
                new[] { pilotName, carName.ToString() },
                laptime);
        }
    }
}
