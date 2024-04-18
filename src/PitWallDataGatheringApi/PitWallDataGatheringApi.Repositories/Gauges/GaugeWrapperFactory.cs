using Microsoft.Extensions.Logging;
using PitWallDataGatheringApi.Repositories.Gauges.Prom;

namespace PitWallDataGatheringApi.Repositories.Gauges
{
    public sealed class GaugeWrapperFactory : IGaugeFactory
    {
        private readonly Dictionary<string, IGauge> _allGauges = new();
        private ILogger<GaugeWrapperFactory> _logger;
        private readonly ILogger<GaugeWrapper> _gaugeLogger;

        private static readonly object LockObject = new();
        
        public GaugeWrapperFactory(ILogger<GaugeWrapperFactory> logger, ILogger<GaugeWrapper> gaugeLogger)
        {
            _logger = logger;
            _gaugeLogger = gaugeLogger;
        }

        public IGauge Create(
            string serieName,
            string description,
            IEnumerable<string> labels)
        {
            var created = CreateGauge(
                serieName,
                description,
                labels);

            return created;
        }

        public IGauge CreateLeaderboardGauge(
            string serieNameFormat,
            string description,
            int positionInRace,
            IEnumerable<string> labels)
        {
            /**
             * The leaderboard is inserted in a SQL database but some of the data is
             * also inserted in a Prometheus gauge.
             */

            string formatedPositionInRace = FormatWithPositionOneLeadingZero(positionInRace);

            string formatedSerieName = string.Format(
                serieNameFormat,
                formatedPositionInRace);

            string formatedDescription = string.Format(
                description,
                formatedPositionInRace);

            var created = CreateGauge(formatedSerieName, formatedDescription, labels);

            return created;
        }

        private static string FormatWithPositionOneLeadingZero(int positionInRace)
        {
            return positionInRace.ToString("D2");
        }

        private IGauge CreateGauge(string serieName, string description, IEnumerable<string> labels)
        {
            lock (LockObject)
            {

                if (_allGauges.TryGetValue(serieName, out IGauge? result))
                {
                    return result;
                }

                var actualSerie = new GaugeWrapper(serieName, description, labels, _gaugeLogger);

                _allGauges.Add(serieName, actualSerie);

                string concatedLabels = string.Join(",", labels);

                _logger.LogInformation($"Prom gauge created - [{serieName}] - [{concatedLabels}] - [{description}]");

                try
                {
                    return _allGauges[serieName];
                }
                catch (KeyNotFoundException e)
                {
                    _logger.LogError(e, $"Unable to find gauge {serieName}");

                    var allGauges = _allGauges.Select(c => c.Key);

                    throw new GaugeNotFoundInDictionaryException(e, allGauges, serieName);
                }
            }
        }

        public IEnumerable<ISerieDocumentation> ListCreated()
        {
            return _allGauges.Values.Cast<ISerieDocumentation>();
        }
    }
}