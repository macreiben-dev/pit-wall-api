using System.Diagnostics.CodeAnalysis;

namespace PitWallDataGatheringApi.Models;

public struct PilotName
{
    private const string NoPilotName = "NoPilotName";
    private string pilotName;

    public PilotName(string? pilotName) => this.pilotName = pilotName ?? NoPilotName;

    public override string ToString() => pilotName;

    public static PilotName Null() => new PilotName(null);

    public static bool operator ==(PilotName left, PilotName right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(PilotName left, PilotName right)
    {
        return !left.Equals(right);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        var comparee = obj as PilotName?;

        if (!comparee.HasValue)
        {
            return false;
        }

        return comparee.Value.ToString() == this.pilotName;
    }

    public override int GetHashCode()
    {
        return this.pilotName.GetHashCode();
    }
}