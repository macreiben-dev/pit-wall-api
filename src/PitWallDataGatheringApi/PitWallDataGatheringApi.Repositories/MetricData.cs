using PitWallDataGatheringApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace PitWallDataGatheringApi.Repositories
{
    public readonly struct MetricData<TData>
    {
        public MetricData(TData? data, PilotName pilotName, CarName carName)
        {
            Data = data;
            CarName = carName;
            PilotName = pilotName;
        }

        public MetricData(TData? data, string pilotName, string carName)
            : this(
                  data,
                  new PilotName(pilotName),
                  new CarName(carName))
        {
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