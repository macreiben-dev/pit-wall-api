using NFluent;
using PitWallDataGatheringApi.Models.Business.Leaderboards;

namespace PitWallDataGatheringApi.Models.Tests.Business.Leaderboards;

public class LeaderboardEntryTest
{
    [Fact]
    public void THEN_carName_is_NA()
    {
        // Arrange
        var entry = new LeaderboardEntry();

        // Act
        var result = entry.CarName;

        // Assert
        Check.That(result).IsEqualTo("NotAvailable");
    }
    
    [Fact]
    public void THEN_pilotName_is_NA()
    {
        // Arrange
        var entry = new LeaderboardEntry();

        // Act
        var result = entry.PilotName;

        // Assert
        Check.That(result).IsEqualTo("NotAvailable");
    }
}