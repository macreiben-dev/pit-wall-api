namespace PitWallDataGatheringApi.Integration.Tests
{
    [Serializable]
    internal class NoResultException : Exception
    {

        public NoResultException(string timeSerieUri, string queryPath) : base($"No data for [{timeSerieUri}] - [{queryPath}]")
        {
            this.TimeSerieUri = timeSerieUri;
            this.QueryPath = queryPath;
        }
        
        public NoResultException(Uri timeSerieUri, string queryPath) : base($"No data for [{timeSerieUri}] - [{queryPath}]")
        {
            this.TimeSerieUri = timeSerieUri.ToString();
            this.QueryPath = queryPath;
        }
        public string TimeSerieUri { get; }
        public string QueryPath { get; }
    }
}