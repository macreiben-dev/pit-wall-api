using PitWallDataGatheringApi.Repositories.Gauges.Sql;

namespace PitWallDataGatheringApi.Repositories.Integration.Tests.Gauges.Sql
{
    public sealed class FakeConnectionString : ILeaderboardConnectionString
    {
        private readonly string connectionString;

        public FakeConnectionString(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public override string ToString()
        {
            return connectionString;
        }
    }
}
