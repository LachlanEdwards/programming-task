using Mantel.Http.Analyser.Exception;
using Mantel.Http.Analyser.Extension;
using Mantel.Http.Analyser.Model;
using Mantel.Http.Analyser.Services;

namespace Mantel.Http.Analyser.UnitTests;

public class EnumerableExtensionUnitTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(999)]
    public void Rank_ReturnsExpectedRankings_Pos(int top)
    {
        // Arrange
        Log[]? logs = LogFileService.GetLogArray("./TestData/example-data.log");
        Assert.NotNull(logs);
        
        //Act
        List<Ranking<string>> rankings = logs.Rank(i => i.Uri, top);
        
        // Assert
        Assert.DoesNotContain(rankings, i => i.Rank > top);
        Assert.Contains(rankings, i => i.Rank <= top);
    }
    
    [Fact]
    public void Rank_ReturnsNoRankings_Neg()
    {
        // Arrange
        Log[] logs = Array.Empty<Log>();
        
        //Act
        List<Ranking<string>> rankings = logs.Rank(i => i.Uri, 3);
        
        // Assert
        Assert.Empty(rankings);
    }
    
    [Fact]
    public void Popularity_ReturnsOrderedPopularity_Pos()
    {
        // Arrange
        Log[]? logs = LogFileService.GetLogArray("./TestData/example-data.log");
        Assert.NotNull(logs);

        //Act
        Popularity<string>[] popularity = logs.Popularity(i => i.IpAddress).ToArray();
        
        // Assert
        for (var i = 0; i < popularity.Length; i++)
        {
            if (i+1 < popularity.Length)
                Assert.True(popularity[i].Count >= popularity[i+1].Count);
        }
    }

    [Fact]
    public void Popularity_ReturnsExpectedValueFirst_Pos()
    {
        // Arrange
        Log[]? logs = LogFileService.GetLogArray("./TestData/example-data.log");
        Assert.NotNull(logs);

        //Act
        Popularity<string>[] popularity = logs.Popularity(i => i.IpAddress).ToArray();
        
        // Assert
        Assert.Equal(4, popularity[0].Count);
        Assert.Equal("168.41.191.40", popularity[0].Value);
    }
    
    [Fact]
    public void Popularity_ReturnsExpectedValueLast_Pos()
    {
        // Arrange
        Log[]? logs = LogFileService.GetLogArray("./TestData/example-data.log");
        Assert.NotNull(logs);

        //Act
        Popularity<string>[] popularity = logs.Popularity(i => i.IpAddress).ToArray();
        
        // Assert
        Assert.Equal(1, popularity[^1].Count);
        Assert.Equal("79.125.00.21", popularity[^1].Value);
    }
    
    [Fact]
    public void Unique_ReturnsOnlyUniqueValues_Pos()
    {
        // Arrange
        Log[]? logs = LogFileService.GetLogArray("./TestData/example-data.log");
        Assert.NotNull(logs);

        //Act
        IEnumerable<string> unique = logs.Unique(i => i.IpAddress).ToArray();
        
        // Assert
        Assert.Equal(unique.ToHashSet().Count, unique.Count());
    }
    
    [Fact]
    public void Unique_ReturnsAllExpectedValues_Pos()
    {
        // Arrange
        Log[]? logs = LogFileService.GetLogArray("./TestData/example-data.log");
        Assert.NotNull(logs);

        //Act
        IEnumerable<string> unique = logs.Unique(i => i.IpAddress).ToArray();
        
        // Assert
        foreach (Log log in logs)
        {
            Assert.Contains(log.IpAddress, unique);
        }
    }
}