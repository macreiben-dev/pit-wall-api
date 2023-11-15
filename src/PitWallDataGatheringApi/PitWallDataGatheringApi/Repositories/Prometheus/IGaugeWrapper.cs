namespace PitWallDataGatheringApi.Repositories.Prometheus
{
    public interface IGauge
    {
        void Update(string label, double? value);
    }
}
