using NFluent;
using PitWallDataGatheringApi.Services;

namespace PitWallDataGatheringApi.Tests.Services
{
    public sealed class CounterExtensionTest
    {
        [Fact]
        public void Given_source_is_null_THEN_noAction()
        {
            double? data = null;

            string flag = null;

            Action action = () => flag = "set";

            data.WhenHasValue(() => action());

            Check.That(flag).IsNull();
        }

        [Fact]
        public void Given_source_is_notNull_THEN_execute_action()
        {
            double? data = 3;

            string flag = null;

            Action action = () => flag = "set";

            data.WhenHasValue(() => action());

            Check.That(flag).IsEqualTo("set");
        }
    }
}
