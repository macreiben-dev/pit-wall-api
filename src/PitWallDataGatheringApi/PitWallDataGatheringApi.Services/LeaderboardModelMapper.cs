using PitWallDataGatheringApi.Models.Apis.v1.Leaderboards;
using BusinessLeaderBoardModel = PitWallDataGatheringApi.Models.Business.Leaderboards.LeaderboardModel;
using IBusinessLeaderBoardModel = PitWallDataGatheringApi.Models.Business.Leaderboards.ILeaderboardModel;
using BusinessLeaderBoardEntry = PitWallDataGatheringApi.Models.Business.Leaderboards.LeaderboardEntry;

namespace PitWallDataGatheringApi.Services
{
    public class LeaderboardModelMapper : ILeaderboardModelMapper
    {
        public IBusinessLeaderBoardModel Map(LeaderboardModel source)
        {
            BusinessLeaderBoardModel output = new BusinessLeaderBoardModel();

            foreach (var item in source.Entries)
            {
                var entry = new BusinessLeaderBoardEntry()
                {
                    LastPitLap = item.LastPitLap,
                    CarClass = item.CarClass,
                    CarNumber = item.CarNumber,
                    Position = item.Position,
                };

                output.Add(entry);
            }

            return output;
        }
    }
}
