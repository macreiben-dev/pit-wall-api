namespace PitWallDataGatheringApi.Repositories
{
    public interface ISerieDocumentation
    {
        public string SerieName { get; }

        public string[] Labels { get; }

        public string Description { get; }
    }
}