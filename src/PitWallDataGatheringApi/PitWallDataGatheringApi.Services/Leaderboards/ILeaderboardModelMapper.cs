﻿using PitWallDataGatheringApi.Models.Business.Leaderboards;

namespace PitWallDataGatheringApi.Services.Leaderboards
{
    public interface ILeaderboardModelMapper
    {
        ILeaderboardModel Map(Models.Apis.v1.Leaderboards.LeaderboardModel source);
    }
}