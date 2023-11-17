using System.Runtime.Serialization;

namespace PitWallDataGatheringApi.Integration.Tests
{
    [Serializable]
    internal class NoDataFoundException : Exception
    {
        public string TimeSerieUri { get; }
        public string QueryPath { get; }

        public NoDataFoundException(string timeSerieUri, string queryPath)
        {
            TimeSerieUri = timeSerieUri;
            QueryPath = queryPath;
        }

        public NoDataFoundException(string? message) : base(message)
        {
        }

        public NoDataFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoDataFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}