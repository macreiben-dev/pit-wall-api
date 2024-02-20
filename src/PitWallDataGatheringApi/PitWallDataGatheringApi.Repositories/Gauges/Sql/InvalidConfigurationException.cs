using System.Runtime.Serialization;

namespace PitWallDataGatheringApi.Repositories.Gauges.Sql
{
    [Serializable]
    internal class NullLeaderboardConfigurationException : Exception
    {
        public NullLeaderboardConfigurationException()
        {
        }

        public NullLeaderboardConfigurationException(string? message) : base(message)
        {
        }

        public NullLeaderboardConfigurationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NullLeaderboardConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}