using PitWallDataGatheringApi.Models.Apis;
using PitWallDataGatheringApi.Repositories;
using Prometheus;

namespace PitWallDataGatheringApi.Services
{
    public sealed class PitwallTelemetryService : IPitwallTelemetryService
    {
        private readonly ITyreWearRepository pitwallTyresPercentRepository;
        private readonly ILaptimeRepository laptimeRepository;

        public PitwallTelemetryService(
            ITyreWearRepository pitwallTyresPercentRepository,
            ILaptimeRepository laptimeRepository)
        {
            this.pitwallTyresPercentRepository = pitwallTyresPercentRepository;
            this.laptimeRepository = laptimeRepository;
        }

        public void Update(TelemetryModel telemetry)
        {
            /**
             * Move all this to a repository because it's only technical stuff.
             * */

            // ------

            if (telemetry != null && telemetry.Tyres != null)
            {
                var tyresWears = telemetry.Tyres;

                pitwallTyresPercentRepository.UpdateFrontLeft(tyresWears);
                pitwallTyresPercentRepository.UpdateFrontRight(tyresWears);
                pitwallTyresPercentRepository.UpdateRearLeft(tyresWears);
                pitwallTyresPercentRepository.UpdateRearRight(tyresWears);
            }

            // ------

            laptimeRepository.Update(telemetry.LaptimeSeconds, telemetry.PilotName);
        }

        private void UpdateGauge(double? data, string gaugeLabel, Gauge gauge)
        {
            if (!data.HasValue)
            {
                return;
            }

            gauge.WithLabels(gaugeLabel).Set(data.Value);
        }
    }
}
