using PitWallDataGatheringApi.Models;
using PitWallDataGatheringApi.Models.Business.Leaderboards;
using System.Collections;

namespace PitWallDataGatheringApi.Services.Tests.Leaderboards
{
    public class FakeLeaderboardModel : ILeaderboardModel
    {
        private readonly IList<ILeaderboardEntry> _entries = new List<ILeaderboardEntry>();

        public PilotName PilotName { get; set; }

        public CarName CarName { get; set; }

        public FakeLeaderboardModel AddEntry(ILeaderboardEntry entry)
        {

            _entries.Add(entry);

            return this;
        }

        public FakeLeaderboardModel WithPilot(string pilotName)
        {
            PilotName = new PilotName(pilotName);
            return this;
        }

        public FakeLeaderboardModel WithCar(string carName)
        {
            CarName = new CarName(carName);

            return this;
        }

        public IEnumerator<ILeaderboardEntry> GetEnumerator()
        {
            return _entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _entries.GetEnumerator();
        }
    }
}
