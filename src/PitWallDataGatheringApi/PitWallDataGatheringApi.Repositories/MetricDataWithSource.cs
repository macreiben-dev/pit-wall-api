using System.Diagnostics.CodeAnalysis;
using PitWallDataGatheringApi.Models;

namespace PitWallDataGatheringApi.Repositories;

/// <summary>
/// This is a copy past from metricData class to tests a solution.
/// </summary>
/// <typeparam name="TData"></typeparam>
public readonly struct MetricDataWithSource<TData>(
    TData? data,
    PilotName pilotName,
    CarName carName,
    PilotName sourcePilotName,
    CarName sourceCarName)
{
    public TData? Data { get; } = data;

    public CarName CarName { get; } = carName;

    public PilotName PilotName { get; } = pilotName;

    public CarName SourceCarName { get; } = sourceCarName;

    public PilotName SourcePilotName { get; } = sourcePilotName;

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        var comparee = obj as MetricDataWithSource<TData>?;

        if (comparee == null)
        {
            return false;
        }

        var intermediary = comparee.Value;

        var isEqual = intermediary.Data != null;
         isEqual &=  intermediary.Data.Equals(Data);
         isEqual &= intermediary.PilotName == PilotName;
         isEqual &= intermediary.CarName == CarName;
         isEqual &= intermediary.SourcePilotName == SourcePilotName;
         isEqual &= intermediary.SourceCarName == SourceCarName;

        return isEqual;
    }
}