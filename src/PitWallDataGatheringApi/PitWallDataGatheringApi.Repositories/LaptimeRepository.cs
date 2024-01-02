using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Repositories.Prom;
using PitWallDataGatheringApi.Repositories.VehicleConsumptions;

namespace PitWallDataGatheringApi.Repositories
{
    public sealed class LaptimeRepository : ILaptimeRepository
    {
        private const string LocalSerieName = "pitwall_laptimes_seconds";
        private readonly IGauge _gaugeLapTimes;
 
        public LaptimeRepository(IGaugeFactory gaugeFactory)
        {
            _gaugeLapTimes = gaugeFactory.Create(
                LocalSerieName,
                "Car laptimes in seconds.",
                ConstantLabels.Labels);
        }

        public void Update(double? data, string pilotName, CarName carName)
        {
            Update(new MetricData<double?>(data, new PilotName(pilotName), carName));
        }

        public void Update(MetricData<double?> metric)
        {
            MetricDataToGauge.Execute(_gaugeLapTimes, metric);
        }
    }
}
