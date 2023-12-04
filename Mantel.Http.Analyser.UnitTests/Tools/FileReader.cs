namespace Mantel.Http.Analyser.UnitTests.Tools;

/// <summary>
/// File Reader.
/// </summary>
public class FileReader
{
    /// <summary>
    /// Get the string content of a file from the specified directory.
    /// </summary>
    /// <param name="path">The file to be opened for reading.</param>
    /// <returns>The string content of the file.</returns>
    public static string? GetFile(string path)
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
        return reader.ReadToEnd();
    }
}