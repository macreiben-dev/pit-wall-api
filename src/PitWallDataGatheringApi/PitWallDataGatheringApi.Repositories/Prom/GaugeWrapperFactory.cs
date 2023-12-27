﻿using Microsoft.Extensions.Logging;

namespace PitWallDataGatheringApi.Repositories.Prom
{
    public sealed class GaugeWrapperFactory : IGaugeFactory
    {
        private readonly Dictionary<string, IGauge> _allGauges = new();
        private ILogger<GaugeWrapperFactory> _logger;
        private readonly ILogger<GaugeWrapper> _gaugeLogger;

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

            string concatedLabels = string.Join(",", labels);

            _logger.LogInformation($"Prom gauge created - [{serieName}] - [{concatedLabels}] - [{description}]");

            return created;
        }

        public IGauge CreateLeaderboardGauge(
            string serieNameFormat,
            string description,
            int positionInRace,
            IEnumerable<string> labels)
        {
            /**
             * Enforce serieNameFormat check with regex
             * */

            var actualSerie = new GaugeWrapper(
                string.Format(
                    serieNameFormat,
                    FormatPositionOneLeadingZero(positionInRace)),
                    description,
                    labels,
                    _gaugeLogger);

            return actualSerie;
        }

        private static string FormatPositionOneLeadingZero(int positionInRace)
        {
            return positionInRace.ToString("D2");
        }

        private IGauge CreateGauge(string serieName, string description, IEnumerable<string> labels)
        {
            if (_allGauges.TryGetValue(serieName, out IGauge? result))
            {
                return result;
            }

            var actualSerie = new GaugeWrapper(serieName, description, labels, _gaugeLogger);

            _allGauges.Add(serieName, actualSerie);

            return _allGauges[serieName];
        }

        public IEnumerable<ISerieDocumentation> ListCreated()
        {
            return _allGauges.Values.Cast<ISerieDocumentation>();
        }
    }
}
