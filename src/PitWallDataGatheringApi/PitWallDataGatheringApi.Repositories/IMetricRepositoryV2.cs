namespace PitWallDataGatheringApi.Repositories
{
    public interface IMetricRepositoryV2<TData>
    {
        void Update(MetricData<TData> metric);
    }
}