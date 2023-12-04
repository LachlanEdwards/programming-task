using Mantel.Http.Analyser.Exception;
using Mantel.Http.Analyser.Extension;
using Mantel.Http.Analyser.Model;
using Mantel.Http.Analyser.Services;
using Mantel.Http.Analyser.UnitTests.Tools;

namespace Mantel.Http.Analyser.UnitTests;

public class ConsoleExtensionUnitTests
{
    [Fact]
    public void WriteToConsole_WritesExpectedValue_Pos()
    {
        // Arrange
        Log[]? logs = LogFileService.GetLogArray("./TestData/example-data.log");
        string? expected = FileReader.GetFile("./TestData/example-console-output.txt");

        Assert.NotNull(logs);
        Assert.NotNull(expected);
        
        List<Ranking<string>> rankings = logs.Rank(i => i.Uri, 3);

        //Act
        using ConsoleCapturer consoleCapturer = new ConsoleCapturer();
        rankings.WriteToConsole("The top 3 most visited URLs:", "visited");
        
        // Assert
        Assert.Equal(expected, consoleCapturer.GetOutput());
    }
}