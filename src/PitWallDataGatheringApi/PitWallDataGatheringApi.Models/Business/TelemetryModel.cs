namespace PitWallDataGatheringApi.Models.Business;

public sealed class TelemetryModel : ITelemetryModel
{
    private ITyresWear _tyreWear;
    private ITyresTemperatures _tyresTemperatures;

    public TelemetryModel()
    {
        TyresWear = new TyresWear();
        TyresTemperatures = new TyresTemperatures();
        VehicleConsumption = new VehicleConsumption();
    }

    public PilotName PilotName { get; set; }
    
    public double? LaptimeSeconds { get; set; }

    public double? AirTemperature { get; set; }

    public double? AvgWetness { get; set; }

    public double? TrackTemperature { get; set; }

    public ITyresWear TyresWear {
        get => _tyreWear;
        set
        {
            if(value == null)
            {
                throw new ArgumentNullException("Tyre wears");
            }

            _tyreWear = value;
        }
    }

    public ITyresTemperatures TyresTemperatures 
    {
        get => _tyresTemperatures;
        set
        {
            if(value == null)
            {
                throw new ArgumentNullException("Tyres temperatures");
            }

            _tyresTemperatures = value;
        }
    }

    public IVehicleConsumption VehicleConsumption { get; set; }
    public CarName CarName { get; set; }
}