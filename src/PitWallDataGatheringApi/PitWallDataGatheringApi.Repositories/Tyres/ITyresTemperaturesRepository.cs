namespace PitWallDataGatheringApi.Repositories.Tyres
{
    public interface ITyresTemperaturesRepository
    {
        void UpdateFrontLeft(MetricData<double?> metric);

        void UpdateFrontRight(MetricData<double?> metric);

        void UpdateRearLeft(MetricData<double?> metric);

        void UpdateRearRight(MetricData<double?> metric);
    }
}