using Mantel.Http.Analyser.Extension;
using Mantel.Http.Analyser.Model;
using Mantel.Http.Analyser.Services;


// Define the path to the log file.
const string path = "./Asset/programming-task-example-data.log";

// Attempt to retrieve logs from the specified file or from the command line arguments.
IEnumerable<Log>? logs = LogFileService.GetLogArray(args.Length < 1 ? path : args[0]);

// Check if logs are null or empty; if so, exit the program.
if (logs is null || !logs.Any())
    return;

// Rank URLs based on the number of times they appear in the logs, and take the top 3.
List<Ranking<string>> urlRanking = logs.Rank(i => i.Uri, 3);

// Rank IP addresses based on their activity (frequency of appearance), and take the top 3.
List<Ranking<string>> ipAddressRanking = logs.Rank(i => i.IpAddress, 3);

// Display the number of unique IP addresses in the logs.
Console.WriteLine($"The number of unique IP addresses: {logs.Unique(i => i.IpAddress).Count()}\n");

// Display the top 3 most visited URLs.
urlRanking.WriteToConsole("The top 3 most visited URLs:", "visited");

// Display the top 3 most active IP addresses.
ipAddressRanking.WriteToConsole("The top 3 most active IP addresses:", "active");

