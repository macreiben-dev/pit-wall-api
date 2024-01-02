namespace PitWallDataGatheringApi.Services
{
    public static class CounterExtension
    {
        public static void WhenHasValue(this double? source, Action action)
             
        {
            if(source != null)
            {
                action();
            }
        }
    }
}
