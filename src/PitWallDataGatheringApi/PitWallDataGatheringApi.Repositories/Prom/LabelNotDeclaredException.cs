namespace PitWallDataGatheringApi.Repositories.Prom
{
    public sealed class LabelNotDeclaredException : Exception
    {
        public LabelNotDeclaredException(string label, double? value)
        {
            LabelName = label;
            Value = value;
        }

        public string LabelName { get; }
        public double? Value { get; }
    }
}
