using System.Diagnostics.CodeAnalysis;

namespace PitWallDataGatheringApi.Models;

public struct CarName
{
    private const string NoCarNumber = "NoCarNumber";
    private string carName;

    public CarName(string? carName) => this.carName = carName ?? NoCarNumber;

    public override string ToString() => carName;
    
    public static CarName Null() => new CarName(null);
    public static bool operator ==(CarName left, CarName right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(CarName left, CarName right)
    {
        return !left.Equals(right);
    }
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        var comparee = obj as CarName?;

        if(!comparee.HasValue)
        {
            return false;
        }

        return comparee.Value.ToString() == this.carName;
    }

    public override int GetHashCode()
    {
        return carName.GetHashCode();
    }
}


public static class StringCarNameExtension
{
    public static CarName AsCarName(this string source)
    {
        return new CarName(source);
    }
}