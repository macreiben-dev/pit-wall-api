namespace PitWallDataGatheringApi.Repositories
{
    public sealed class SimerKeyRepository : ISimerKeyRepository
    {
        private readonly string _key;

        public SimerKeyRepository()
        {
            _key = Environment.GetEnvironmentVariable("VortexSimerKey");
        }

        public string Key => _key;
    }
}
