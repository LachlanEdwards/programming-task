using Mantel.Http.Analyser.Model;

namespace Mantel.Http.Analyser.Extension;

/// <summary>
/// Console Extensions.
/// </summary>
public static class ConsoleExtensions
{
    /// <summary>
    /// Writes a formatted list of rankings to the console.
    /// </summary>
    /// <typeparam name="T">The type of elements in the ranking list.</typeparam>
    /// <param name="rankings">The list of rankings to be displayed.</param>
    /// <param name="title">The title to be displayed before the rankings.</param>
    /// <param name="descriptor">The descriptor to be used in the ranking display.</param>
    public static void WriteToConsole<T>(this List<Ranking<T>> rankings, string title, string descriptor)
    {
        // Display the provided title.
        Console.WriteLine($"{title}");

        // HashSet to track unique positions in rankings.
        HashSet<int> position = new HashSet<int>();

        // Iterate through each ranking in the list.
        foreach (Ranking<T> ranking in rankings)
        {
            // Determine the appropriate prepend string based on position uniqueness.
            string prepend = position.Contains(ranking.Rank) ? "    " : $"(#{ranking.Rank})";

            // Display the ranking information.
            Console.WriteLine($"{prepend} \"{ranking.Popularity.Value}\" {descriptor} {ranking.Popularity.Count}x.");

            // Add the position to the HashSet to track uniqueness.
            position.Add(ranking.Rank);
        }

        // Display a newline for better readability.
        Console.WriteLine($"\n");
    }
}