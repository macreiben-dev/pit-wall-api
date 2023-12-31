﻿using PitWallDataGatheringApi.Models.Apis.v1.Leaderboards;
using BusinessLeaderBoardModel = PitWallDataGatheringApi.Models.Business.Leaderboards.LeaderboardModel;
using IBusinessLeaderBoardModel = PitWallDataGatheringApi.Models.Business.Leaderboards.ILeaderboardModel;
using BusinessLeaderBoardEntry = PitWallDataGatheringApi.Models.Business.Leaderboards.LeaderboardEntry;

namespace PitWallDataGatheringApi.Services.Leaderboards
{
    public class LeaderboardModelMapper : ILeaderboardModelMapper
    {
        public IBusinessLeaderBoardModel Map(LeaderboardModel source)
        {
            BusinessLeaderBoardModel output = new BusinessLeaderBoardModel();

            output.PilotName = new Models.PilotName(source.PilotName);

            output.CarName = new Models.CarName(source.CarName);

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
