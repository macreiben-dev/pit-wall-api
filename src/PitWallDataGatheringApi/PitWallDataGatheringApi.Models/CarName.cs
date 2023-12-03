namespace PitWallDataGatheringApi.Models;

public struct CarName
{
    private const string NoCarNumber = "NoCarNumber";
    private string carName;

    public CarName(string? carName) => this.carName = carName ?? NoCarNumber;

    public override string ToString() => carName;
    

    public static CarName Null() => new CarName(null);

}

public struct PilotName
{
    private const string NoPilotName = "NoPilotName";
    private string pilotName;

    public PilotName(string? pilotName) => this.pilotName = pilotName ?? NoPilotName;

    public override string ToString() => pilotName;

    public static PilotName Null() => new PilotName(null);
}