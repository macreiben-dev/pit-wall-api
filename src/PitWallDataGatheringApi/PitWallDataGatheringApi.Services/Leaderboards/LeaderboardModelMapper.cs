using PitWallDataGatheringApi.Models.Apis.v1.Leaderboards;
using BusinessLeaderBoardModel = PitWallDataGatheringApi.Models.Business.Leaderboards.LeaderboardModel;
using IBusinessLeaderBoardModel = PitWallDataGatheringApi.Models.Business.Leaderboards.ILeaderboardModel;
using BusinessLeaderBoardEntry = PitWallDataGatheringApi.Models.Business.Leaderboards.LeaderboardEntry;

namespace PitWallDataGatheringApi.Services.Leaderboards
{
    public class LeaderboardModelMapper : ILeaderboardModelMapper
    {
        private const string NotAvailable = "NA";

        public IBusinessLeaderBoardModel Map(LeaderboardModel source)
        {
            BusinessLeaderBoardModel output = new();

            output.PilotName = new(source.PilotName);

            output.CarName = new(source.CarName);

            foreach (var item in source.Entries)
            {
                var entry = new BusinessLeaderBoardEntry()
                {
                    LastPitLap = item.LastPitLap,
                    CarClass = item.CarClass ?? NotAvailable,
                    CarNumber = item.CarNumber ?? NotAvailable,
                    CarName = item.CarName ?? NotAvailable,
                    Position = item.Position,
                    InPitLane = item.InPitLane
                };

                output.Add(entry);
            }

            return output;
        }
    }
}
