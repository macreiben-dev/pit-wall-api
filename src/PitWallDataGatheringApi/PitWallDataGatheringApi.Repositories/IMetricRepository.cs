namespace PitWallDataGatheringApi.Repositories
{
    public interface IMetricRepository<TData>
    {
        void Update(MetricData<TData> metric);
    }
}