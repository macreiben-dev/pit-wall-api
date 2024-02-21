namespace PitWallDataGatheringApi.Repositories.Gauges.Sql
{
    public sealed class LeaderboardConnectionString : ILeaderboardConnectionString
    {
        private readonly string? _connectionString;

        public LeaderboardConnectionString()
        {
            _connectionString = Environment.GetEnvironmentVariable("DatabaseLeaderboardDatabase");
        }

        public string Value => ToString();

        public override string ToString()
        {
            if (_connectionString != null)
            {
                return _connectionString;
            }

            throw new NullLeaderboardConfigurationException();
        }
    }
}
