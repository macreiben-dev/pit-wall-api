using PitWallDataGatheringApi.Models;
using System.Diagnostics.CodeAnalysis;

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

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            var comparee = obj as MetricData<TData>?;

            var intermediary = comparee.Value;

            var isEqual = intermediary.Data.Equals(Data)
                && intermediary.PilotName == PilotName
                && intermediary.CarName == CarName;

            return isEqual;
        }
    }
}