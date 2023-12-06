using System.Collections;
using IBusinessLeaderBoardModel = PitWallDataGatheringApi.Models.Business.Leaderboards.ILeaderboardModel;
using IBusinessLeaderBoardEntry = PitWallDataGatheringApi.Models.Business.Leaderboards.ILeaderboardEntry;

namespace PitWallDataGatheringApi.Models.Business.Leaderboards
{
    public sealed class LeaderboardModel : IBusinessLeaderBoardModel
    {
        private readonly List<IBusinessLeaderBoardEntry> _source;

        public LeaderboardModel()
        {

            _source = new List<IBusinessLeaderBoardEntry>();
        }

        public void Add(IBusinessLeaderBoardEntry entry)
        {
            _source.Add(entry);
        }

        public IEnumerator<IBusinessLeaderBoardEntry> GetEnumerator()
        {
            return _source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
