namespace PitWallDataGatheringApi.Repositories.Tyres
{
    public interface ITyreWearRepositoryV2
    {
        void UpdateFrontLeft(MetricData<double?> metric);

        void UpdateFrontRight(MetricData<double?> metric);

        void UpdateRearLeft(MetricData<double?> metric);

        void UpdateRearRight(MetricData<double?> metric);
    }
}