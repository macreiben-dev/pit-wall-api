namespace PitWallDataGatheringApi.Repositories.Prom
{
    public sealed class GaugeWrapperFactory : IGaugeWrapperFactory
    {
        private readonly Dictionary<string, IGauge> _allGauges = new();

        public IGauge Create(
            string serieName,
            string description,
            IEnumerable<string> labels)
        {
            if (_allGauges.TryGetValue(serieName, out IGauge? result))
            {
                return result;
            }

            var actualSerie = new GaugeWrapper(serieName, description, labels);

            _allGauges.Add(serieName, actualSerie);

            return _allGauges[serieName];
        }

        public IEnumerable<ISerieDocumentation> ListCreated()
        {
            return _allGauges.Values.Cast<ISerieDocumentation>();
        }
    }
}
