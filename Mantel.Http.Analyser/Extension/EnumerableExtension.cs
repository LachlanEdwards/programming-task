using Mantel.Http.Analyser.Model;

namespace Mantel.Http.Analyser.Extension;

/// <summary>
/// Enumerable Extensions.
/// </summary>
public static class EnumerableExtension
{
    /// <summary>
    /// Calculates the popularity of values in a sequence based on a specified key selector.
    /// </summary>
    /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
    /// <typeparam name="TValue">The type of the values to determine popularity.</typeparam>
    /// <param name="list">The source sequence of elements.</param>
    /// <param name="func">A function to extract the key for each element.</param>
    /// <returns>An IEnumerable of tuples containing the unique values and their respective popularity counts.</returns>
    public static IEnumerable<Popularity<TValue>> Popularity<TSource, TValue>(this IEnumerable<TSource> list,
        Func<TSource, TValue> func)
        where TValue : notnull
    {
        // Dictionary to store the count of each unique value.
        Dictionary<TValue, int> dictionary = new Dictionary<TValue, int>();

        // Iterate through the source sequence and populate the dictionary.
        foreach (TValue value in list.Select(func.Invoke))
        {
            // Initialize the count for a new value if not present.
            dictionary.TryAdd(value, 0);

            // Increment the count for the current value.
            dictionary[value] += 1;
        }

        // Return the unique values and their counts in descending order of count
        return from i in dictionary orderby i.Value descending select new Popularity<TValue>(i.Key, i.Value);
    }

    /// <summary>
    /// Retrieves the unique values from a sequence based on a specified key selector.
    /// </summary>
    /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
    /// <typeparam name="TValue">The type of the values to make unique.</typeparam>
    /// <param name="list">The source sequence of elements.</param>
    /// <param name="func">A function to extract the key for each element.</param>
    /// <returns>An IEnumerable of unique values from the source sequence.</returns>
    public static IEnumerable<TValue> Unique<TSource, TValue>(this IEnumerable<TSource> list,
        Func<TSource, TValue> func)
    {
        // HashSet to store unique values.
        HashSet<TValue> hashSet = new HashSet<TValue>();

        // Iterate through the source sequence and add unique values to the HashSet.
        foreach (TValue value in list.Select(func.Invoke))
            hashSet.Add(value);

        // Return the unique values as an array.
        return hashSet.ToArray();
    }

    /// <summary>
    /// Ranks the values in a sequence based on their popularity calculated using the specified key selector.
    /// </summary>
    /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
    /// <typeparam name="TValue">The type of values to be ranked.</typeparam>
    /// <param name="list">The source sequence of elements.</param>
    /// <param name="func">A function to extract the key for calculating popularity.</param>
    /// <param name="top">The maximum number of rankings to return.</param>
    /// <returns>A list of <see cref="Ranking{TValue}"/> objects representing the ranked values based on their popularity.</returns>
    /// <remarks>
    /// Popularity is determined based on the count of occurrences of each value in the sequence.
    /// </remarks>
    public static List<Ranking<TValue>> Rank<TSource, TValue>(
        this IEnumerable<TSource> list,
        Func<TSource, TValue> func,
        int top)
        where TValue : notnull
    {
        // Create a list to store the rankings.
        List<Ranking<TValue>> rankings = new List<Ranking<TValue>>();

        // Calculate the popularity of values in the sequence using the Popularity extension method.
        IEnumerable<Popularity<TValue>> sorted = list.Popularity(func);

        // Group the sorted popularity values based on their count.
        IGrouping<int, Popularity<TValue>>[] groups = sorted.GroupBy(i => i.Count).ToArray();

        // Iterate through the groups to assign ranks to each value.
        for (var i = 0; i < groups.Length; i++)
        {
            // Calculate the rank for the current group.
            int rank = i + 1;

            // Get the current group of popularity values.
            IGrouping<int, Popularity<TValue>> group = groups[i];

            // Get the items in the current group.
            IEnumerable<Popularity<TValue>> items = group;

            // Add rankings for each item in the group to the overall rankings list.
            rankings.AddRange(items.Select(item => new Ranking<TValue>(rank, item)));
            
            // Check if the desired number of rankings has been reached and exit the loop if so.
            if (items.Count() + rankings.Count >= top) break;
        }

        // Return the final list of rankings.
        return rankings;
    }
}