using PitWallDataGatheringApi.Models.Apis.v1.Leaderboards;
using BusinessLeaderBoardModel = PitWallDataGatheringApi.Models.Business.Leaderboards.LeaderboardModel;
using IBusinessLeaderBoardModel = PitWallDataGatheringApi.Models.Business.Leaderboards.ILeaderboardModel;
using BusinessLeaderBoardEntry = PitWallDataGatheringApi.Models.Business.Leaderboards.LeaderboardEntry;
using IBusinessLeaderBoardEntry = PitWallDataGatheringApi.Models.Business.Leaderboards.ILeaderboardEntry;

namespace PitWallDataGatheringApi.Services
{
    public class LeaderboardModelMapper
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
                    CarNumber = item.CarNumber
                };

                output.Add(entry);
            }

            return output;
        }
    }
}
