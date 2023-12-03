using PitWallDataGatheringApi.Models;

namespace PitWallDataGatheringApi.Repositories
{
    public readonly struct MetricData<TData>
    {
        public MetricData(TData? data, CarName carName, PilotName pilotName)
        {
            Data = data;
            CarName = carName;
            PilotName = pilotName;
        }

        public TData? Data { get; }

        public CarName CarName { get; }

        public PilotName PilotName { get; }
    }
}