using PitWallDataGatheringApi.Repositories.Prometheus;

namespace PitWallDataGatheringApi.Repositories.WeatherConditions
{
    public sealed class AvgWetnessRepository : IDocumentationAvgWetnessSerie, IAvgWetnessRepository
    {
        private const string LocalSerieName = "pitwall_road_wetness_avg_percent";
        private readonly IGauge _gauge;
        readonly string[] _labels = new[] { "Pilot", "All", "Car" };

        public string SerieName => LocalSerieName;

        public string[] Labels => _labels;

        public string Description => "Road wetness in percent.";

        public AvgWetnessRepository(IGaugeWrapperFactory gaugeFactory)
        {
            _gauge = gaugeFactory.Create(
               LocalSerieName,
              "Average road wetness in percent.",
              Labels);
        }

        public void Update(double? laptime, string pilotName)
        {
            _gauge.Update(
                ConstantLabels.Labels,
                laptime);
        }
    }
}
