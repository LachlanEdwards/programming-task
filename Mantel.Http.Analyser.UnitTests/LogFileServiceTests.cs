using Mantel.Http.Analyser.Exception;
using Mantel.Http.Analyser.Model;
using Mantel.Http.Analyser.Services;

namespace Mantel.Http.Analyser.UnitTests;

public class LogFileServiceTests
{
    [Fact]
    public void GetLogArray_Returns23Results_Pos()
    {
        // Act
        Log[]? logs = LogFileService.GetLogArray("./TestData/example-data.log");
        
        // Assert
        Assert.NotNull(logs);
        Assert.Equal(23, logs.Length);
    }
    
    [Fact]
    public void GetLogArray_Returns0Results_Neg()
    {
        // Act
        Log[]? logs = LogFileService.GetLogArray("./TestData/empty-example-data.log");
        
        // Assert
        Assert.NotNull(logs);
        Assert.Empty(logs);
    }
    
    [Fact]
    public void GetLogArray_ReturnsNull_Neg()
    {
        // Act
        Log[]? logs = LogFileService.GetLogArray("./TestData/does-not-exist.log");
        
        // Assert
        Assert.Null(logs);
    }
    
    [Theory]
    [InlineData("./TestData/invalid-date-time-format-example-data.log")]
    [InlineData("./TestData/oor-status-code-example-data.log")]
    [InlineData("./TestData/no-ip-example-data.log")]
    [InlineData("./TestData/malformatted-example-data.log")]
    [InlineData("./TestData/invalid-int-example-data.log")]
    public void GetLogArray_ThrowsCaptureException_Neg(string path)
    {
        // Act & Assert
        Assert.Throws<CaptureException>(() => LogFileService.GetLogArray(path));
    }
    
    [Theory]
    [InlineData("./TestData/no-newline-example-data.log")]
    public void GetLogArray_ThrowsException_Neg(string path)
    {
        // Act
        Log[]? logs = LogFileService.GetLogArray(path);
        
        // Act & Assert
        Assert.NotNull(logs);
        Assert.Single(logs);
    }
}