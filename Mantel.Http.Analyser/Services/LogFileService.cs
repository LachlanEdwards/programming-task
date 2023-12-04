using Mantel.Http.Analyser.Model;

namespace Mantel.Http.Analyser.Services;

/// <summary>
/// Log File Service.
/// </summary>
public static class LogFileService
{
    /// <summary>
    /// Retrieves an array of Log objects from a specified file path.
    /// </summary>
    /// <param name="path">The path to the file containing log information.</param>
    /// <returns>An array of Log objects parsed from the log file.</returns>
    public static Log[]? GetLogArray(string path)
    {
        // If the file does not exist, writes an error to the Console and return a null value.
        if (!File.Exists(path))
        {
            Console.WriteLine($"ERROR: File not found in directory {path}");
            return default;
        }

        // Open the file stream for reading.
        FileStream fileStream = File.OpenRead(path);

        // Use a StreamReader to read the contents of the file.
        using StreamReader reader = new StreamReader(fileStream);

        // Read the entire content of the file into a string.
        string output = reader.ReadToEnd();

        // Split the string into an array of log entries based on newline characters, and remove any empty entries.
        string[] logs = output.Split("\n", StringSplitOptions.RemoveEmptyEntries);

        // Parse each log entry string into a Log object using the Log.Parse method, and convert the result into an array.
        return logs.Select(Log.Parse).ToArray();
    }

}