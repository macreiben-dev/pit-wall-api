namespace PitWallDataGatheringApi.Repositories
{
    public interface ILaptimeRepository
    {
        void Update(double? laptime, string pilotName);
    }
}