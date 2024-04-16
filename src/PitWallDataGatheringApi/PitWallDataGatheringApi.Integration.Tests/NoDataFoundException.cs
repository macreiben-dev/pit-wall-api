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
    }
}