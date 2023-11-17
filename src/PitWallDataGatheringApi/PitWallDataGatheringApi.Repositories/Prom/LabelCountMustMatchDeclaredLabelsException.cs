namespace PitWallDataGatheringApi.Repositories.Prom
{
    public sealed class LabelCountMustMatchDeclaredLabelsException : Exception
    {
        public LabelCountMustMatchDeclaredLabelsException(
            IEnumerable<string> declared,
            IEnumerable<string> received)
        {
            Declared = declared;
            Received = received;
        }

        public IEnumerable<string> Declared { get; }
        public IEnumerable<string> Received { get; }
    }
}
